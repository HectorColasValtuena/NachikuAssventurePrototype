using UnityEngine;

using ControllerProvider = ASSPhysics.ControllerSystem.ControllerProvider;

using RectMath = ASSistant.ASSMath.RectMath;
using static ASSPhysics.CameraSystem.CameraExtensions; //Camera.EMRectFromOrthographicCamera();

namespace ASSPhysics.CameraSystem
{
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(Camera))]
	public abstract class RectCameraControllerBase : MonoBehaviour, IViewportController
	{
	//serialized fields
		[SerializeField]
		private Rect viewportLimits; //camera boundaries

		[SerializeField]
		private bool autoConfigureLimits = true; //if true gather limits from scene
	//ENDOF serialized fields

	//private fields
		protected Camera cameraComponent; //cached reference to the camera this controller handles
		private RectTransform rectTransform;
	//ENDOF private fields

	//public properties
		public virtual Rect rect
		{
			get { return rectTransform.rect; }
			protected set
			{
				//first ensure rect fulfills side ratio and clamp its position within viewport limits
				Rect newRect = ClampRectWithinLimits(CreateCameraRect(sampleRect: value));
				//then apply validated rect to the transform
				rectTransform.rect.Set(
					x: newRect.x,
					y: newRect.y,
					width: newRect.width,
					height: newRect.height
				);
			}
		}
	//ENDOF public properties

	//private properties
		private Vector2 position
		{
			get { return rect.position; }
		}

		private float screenRatio
		{
			get { return cameraComponent.pixelWidth / cameraComponent.pixelHeight; }
		}
	//ENDOF private properties

	//IViewportController implementation
	  //dimensions and position of the viewport
		Rect IViewportController.rect
		{
			get { return rect; }
			set { rect = value; }
		}

	  //current height value of the viewport
		float IViewportController.size
		{
			get { return rect.height; }
			set { rect = CreateCameraRect(height: value); }
		}

	  //current position
		Vector2 IViewportController.position
		{
			get { return position; }
			set { rect = CreateCameraRect(position: value); }
		}

		//transforms a screen point into a world position
		//if worldSpace is false, the returned Vector3 ignores camera transform position
		Vector2 IViewportController.ScreenSpaceToWorldSpace (
			Vector2 screenPosition,
			bool worldSpace
		) {
			//normalize position into a 0-1 range
			screenPosition = Vector2.Scale(screenPosition, new Vector2 (1/Screen.width, 1/Screen.height));

			//multiply normalized position by camera size
			Vector2 cameraSize = new Vector2 (rect.width, rect.height);
			screenPosition = Vector2.Scale(screenPosition, cameraSize);

			//finally correct world position if necessary
			if (worldSpace)
			{
				screenPosition = screenPosition + position - (cameraSize/2);
			}

			return screenPosition;
		}

		//Prevents position from going outside of this camera's boundaries
		Vector2 IViewportController.ClampPositionToViewport (Vector2 position)
		{ return RectMath.ClampVector2WithinRect(position, rect); }
		Vector3 IViewportController.ClampPositionToViewport (Vector3 position)
		{ return RectMath.ClampVector3WithinRect(position, rect); }
	//ENDOF IViewportController implementation

	//MonoBehaviour lifecycle implementation
		public void Awake ()
		{
			Initialize();
		}

		public void OnPreCull ()
		{
			ApplyCameraSize();
		}

		public void OnDestroy ()
		{
			ControllerProvider.DisposeController<IViewportController>(this);
		}
	//ENDOF MonoBehaviour lifecycle implementation

	//private methods
		//Controller initialization
		private void Initialize ()
		{
			//report this controller to the provider
			ControllerProvider.RegisterController<IViewportController>(this);

			//cache references to Camera and RectTransform components
			cameraComponent = GetComponent<Camera>();
			rectTransform = (RectTransform) transform;

			//initialize limits
			if (autoConfigureLimits) { viewportLimits = cameraComponent.EMRectFromOrthographicCamera(); }
		}

		private void ApplyCameraSize ()
		{
			cameraComponent.orthographicSize = rect.height / 2;
		}
	//ENDOF private methods

	//inheritable private methods
		//creates previewing camera dimensions at target position and height.
		//non included parameters are filled with current camera values
		//Rect width is inferred off of height and screen ratio.
		protected Rect CreateCameraRect (Vector2? position = null, float? height = null)
		{
			//first validate and complete inputs
			Vector2 validPosition = (position != null) 
				?	(Vector2) position
				:	rect.position;
			float validHeight = (height != null) 
				?	(float) height
				:	rect.height;

			//now create and return a rect with proper dimensions and position
			return new Rect(
				x: validPosition.x,
				y: validPosition.y,
				width: validHeight * screenRatio,
				height: validHeight
			);
		}
		protected Rect CreateCameraRect (Rect sampleRect)
		{
			return CreateCameraRect(position: sampleRect.position, height: sampleRect.height);
		}

		//clamps a rect's height and position to make it fit within viewport limits
		protected Rect ClampRectWithinLimits (Rect innerRect)
		{
			return RectMath.TrimAndClampRectWithinRect(innerRect: innerRect, outerRect: viewportLimits);			
		}
	//ENDOF inheritable private methods
	}
}