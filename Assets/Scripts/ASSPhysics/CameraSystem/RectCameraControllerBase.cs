using UnityEngine;

using ControllerProvider = ASSPhysics.ControllerSystem.ControllerProvider;

using RectMath = ASSistant.ASSMath.RectMath;
using static ASSPhysics.CameraSystem.CameraExtensions; //Camera.EMRectFromOrthographicCamera();

namespace ASSPhysics.CameraSystem
{
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(Camera))]
	public abstract class RectCameraControllerBase : ViewportControllerBase
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

	//inherited property implementation
		protected override Rect publicRect { get { return rect; } }
	//ENDOF inherited property implementation

	//protected class properties
		protected virtual Rect rect
		{
			get { return rectTransform.rect; }
			set
			{
				//apply a pre-validated rect to the transform
				rectTransform.EMSetRect(
					ClampRectWithinLimits( //clamp rect position within viewport limits
						CreateCameraRect(sampleRect: value))); //ensure rect fulfills size ratio
			}
		}
	//protected class properties

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

	//inherited method implementation
		//moves and resizes camera viewport
		//if only one of the parameters is used the other aspect of the viewport is unchanged
		protected override void ChangeViewport (Vector2? position, float? size)
		{
			rect = CreateCameraRect(position: position, height: size);
		}
	//ENDOF inherited method implementation

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

	//protected class methods
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