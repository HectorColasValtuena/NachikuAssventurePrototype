using UnityEngine;

using ControllerProvider = ASSPhysics.ControllerSystem.ControllerProvider;

namespace ASSPhysics.CameraSystem
{
	public abstract class OrthgraphicCameraControllerBase : MonoBehaviour, IViewportController
	{
		//serialized fields
			[SerializeField]
			new private Camera camera; //cached reference to the camera this controller handles

			[SerializeField]
			private Vector3 cameraDepthCorrection = new Vector3 (0f, 0f, 10f); //camera Z depth correction
		//ENDOF serialized fields

		//IViewportController implementation
			public abstract Rect baseViewport { get; } //original size of the viewport
			public virtual Rect currentViewport { get; protected set; } //current size of the viewport

			//transforms a Vector3 representing a screen point into a Vector3 representing the 2d position
			//if originIsCamera is true, the returned Vector3 originates in the camera's position
			public Vector3 ScreenSpaceToWorldSpace (
				Vector3 screenPosition,
				Camera pivotCamera = null,
				bool originIsCamera = false
			) {
				if (pivotCamera == null) { pivotCamera = Camera.main; }	

				//normalize position into a 0-1 range
				Vector3 position = Vector3.Scale(screenPosition, new Vector3 (1/Screen.width, 1/Screen.height, 0f));

				//multiply normalized position by camera size
				position = Vector3.Scale(position, GetCameraSize(pivotCamera));

				//finally correct world position if necessary
				if (!originIsCamera)
				{
					position = position + pivotCamera.transform.position + cameraDepthCorrection - (GetCameraSize(pivotCamera)/2);
				}

				return position;
			}
		//ENDOF IViewportController implementation

		//MonoBehaviour lifecycle implementation
			public void Awake ()
			{
				if (camera == null) { camera = GetComponent<Camera>(); }
				currentViewport = camera.EMRectFromOrthographicCamera();

				ControllerProvider.RegisterController<IViewportController>(this);
			}

			public void OnDestroy ()
			{
				ControllerProvider.DisposeController<IViewportController>(this);
			}
		//ENDOF MonoBehaviour lifecycle implementation

		//private methods
			//updates the cached viewport for the active camera			
			protected void UpdateCurrentViewport ()
			{
				currentViewport = camera.EMRectFromOrthographicCamera();
			}

			//calculates cameraSize
			private static Vector3 GetCameraSize (Camera pivotCamera) 
			{
				return new Vector3 (pivotCamera.orthographicSize * pivotCamera.aspect * 2,	pivotCamera.orthographicSize * 2, 0f);
			}
		//ENDOF private methods
	}
}