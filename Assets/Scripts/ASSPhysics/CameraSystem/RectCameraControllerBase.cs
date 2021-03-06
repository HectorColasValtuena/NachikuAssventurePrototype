using UnityEngine;

using ControllerProvider = ASSPhysics.ControllerSystem.ControllerProvider;

using RectMath = ASSistant.ASSMath.RectMath;
using static ASSPhysics.CameraSystem.CameraExtensions; //Camera.EMRectFromOrthographicCamera();

namespace ASSPhysics.CameraSystem
{
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(Camera))]
	public class RectCameraControllerBase : ViewportControllerBase
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

	//abstract property implementation
		protected override Rect viewportRect { get { return rect; }}
	//ENDOF abstract property implementation

	//protected class properties
		protected virtual Rect rect
		{
			get { return rectTransform.EMGetWorldRect(); }
			set
			{
				//apply a pre-validated rect to the transform
				rectTransform.EMSetRect(ValidateCameraRect(value));
				/////Maybe this doesn't need to create a new rect, only change rect width
			}
		}
	//protected class properties

	//private properties
		private float screenRatio
		{
			get { return cameraComponent.aspect; }
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
		public override void Awake ()
		{
			base.Awake();
			Initialize();
		}

		public void OnPreCull ()
		{
			ApplyCameraSize();
		}
	//ENDOF MonoBehaviour lifecycle implementation

	//private methods
		//Controller initialization
		private void Initialize ()
		{
			//cache references to Camera and RectTransform components
			cameraComponent = GetComponent<Camera>();
			rectTransform = (RectTransform) transform;

			//initialize limits
			if (autoConfigureLimits) { viewportLimits = cameraComponent.EMRectFromOrthographicCamera(); }
		}

		//applies the rect height to the camera component right before rendering
		private void ApplyCameraSize ()
		{
			cameraComponent.orthographicSize = rect.height / 2;
		}
	//ENDOF private methods

	//protected class methods
		//Clamps and properly sizes a rect for this camera ratio and limits
		protected Rect ValidateCameraRect (Rect innerRect)
		{
			//clamp rect position within viewport limits
			return ClampRectWithinLimits(
				//ensure rect fulfills size ratio
				CreateCameraRect(sampleRect: innerRect)
			);
		}

		//creates previewing camera dimensions at target position and height.
		//non included parameters are filled with current camera values
		//Rect width is inferred off of height and screen ratio.
		protected Rect CreateCameraRect (Rect sampleRect)
		{ return CreateCameraRect(position: sampleRect.center, height: sampleRect.height); }
		protected Rect CreateCameraRect (Vector2? position = null, float? height = null)
		{
			//first validate and complete inputs
			Vector2 validPosition = (position != null) 
				?	(Vector2) position
				:	rect.center;
			float validHeight = (height != null) 
				?	(float) height
				:	rect.height;

			//Debug.Log("CreateCameraRect(" + position + ", " + height + ")");
			//Debug.Log(" validPosition: " + validPosition + "\n validHeight: " + validHeight);

////////////////[TO-DO] this is a bit duplicate logic, condense this and CameraExtensions.EMRectFromOrthographicCamera()?
			
			//now create and return a rect with proper dimensions and position
			return RectMath.RectFromCenterAndSize(
				position: validPosition,
				width: validHeight * screenRatio,
				height: validHeight
			);
		}

		//clamps a rect's height and position to make it fit within viewport limits
		protected Rect ClampRectWithinLimits (Rect innerRect)
		{
			return RectMath.TrimAndClampRectWithinRect(innerRect: innerRect, outerRect: viewportLimits);			
		}
	//ENDOF inheritable private methods

	//////////////////////////////////////////////////////////////////
	}
}