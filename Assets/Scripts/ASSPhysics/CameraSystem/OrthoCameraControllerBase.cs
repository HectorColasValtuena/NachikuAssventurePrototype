using UnityEngine;

using ControllerProvider = ASSPhysics.ControllerSystem.ControllerProvider;

namespace ASSPhysics.CameraSystem
{
	public abstract class OrthoCameraControllerBase : MonoBehaviour, IViewportController
	{
		//serialized fields
			[SerializeField]
			protected Camera cameraComponent; //cached reference to the camera this controller handles

			[SerializeField]
			private Vector3 cameraDepthCorrection = new Vector3 (0f, 0f, 10f); //camera Z depth correction
		//ENDOF serialized fields

		//IViewportController implementation
			public virtual Rect baseViewport { get; private set; } //original size of the viewport
			public abstract Rect currentViewport { get; protected set; } //current size of the viewport
			public float viewportHeight { get { return cameraComponent.orthographicSize * 2; } }	//current height value of the viewport

			//transforms a Vector3 representing a screen point into a Vector3 representing the 2d position
			//if worldSpace is false, the returned Vector3 ignores camera transform position
			public Vector3 ScreenSpaceToWorldSpace (
				Vector3 screenPosition,
				Camera pivotCamera,
				bool worldSpace
			) {
				if (pivotCamera == null) { pivotCamera = cameraComponent; }	

				//normalize position into a 0-1 range
				Vector3 position = Vector3.Scale(screenPosition, new Vector3 (1/Screen.width, 1/Screen.height, 0f));

				//multiply normalized position by camera size
				position = Vector3.Scale(position, GetCameraSize(pivotCamera));

				//finally correct world position if necessary
				if (worldSpace)
				{
					position = position + pivotCamera.transform.position + cameraDepthCorrection - (GetCameraSize(pivotCamera)/2);
				}

				return position;
			}
		//ENDOF IViewportController implementation

		//MonoBehaviour lifecycle implementation
			public void Awake ()
			{
				if (cameraComponent == null) { cameraComponent = GetComponent<Camera>(); }
				ControllerProvider.RegisterController<IViewportController>(this);
			}

			public virtual void Start ()
			{
				baseViewport = cameraComponent.EMRectFromOrthographicCamera();
			}

			public void OnDestroy ()
			{
				ControllerProvider.DisposeController<IViewportController>(this);
			}
		//ENDOF MonoBehaviour lifecycle implementation

		//private methods
			/*
			//updates the cached viewport for the active camera			
			protected void UpdateCurrentViewport ()
			{
				currentViewport = camera.EMRectFromOrthographicCamera();
			}
			*/
			//calculates cameraSize
			private static Vector3 GetCameraSize (Camera pivotCamera) 
			{
				return new Vector3 (pivotCamera.orthographicSize * pivotCamera.aspect * 2,	pivotCamera.orthographicSize * 2, 0f);
			}
		//ENDOF private methods
	}
}