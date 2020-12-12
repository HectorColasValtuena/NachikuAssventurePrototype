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
			public virtual Rect rect { get; protected set; } //current size of the viewport

			public virtual float size //current height value of the viewport
			{
				get { return cameraComponent.orthographicSize; }
				set
				{
					cameraComponent.orthographicSize = value; 
					UpdateRect();
				}
			}

			public virtual Vector3 position
			{
				get { return cameraComponent.transform.position; }
				set 
				{
					cameraComponent.transform.position = value;
					UpdateRect();
				}
			}

			//transforms a Vector3 representing a screen point into a Vector3 representing the 2d position
			//if worldSpace is false, the returned Vector3 ignores camera transform position
			public Vector3 ScreenSpaceToWorldSpace (
				Vector3 screenPosition,
				bool worldSpace
			) {
				//Vector3 cameraSize { get { return new Vector3 (rect.width, rect.height, 0f); }}

				//normalize position into a 0-1 range
				Vector3 position = Vector3.Scale(screenPosition, new Vector3 (1/Screen.width, 1/Screen.height, 0f));

				//multiply normalized position by camera size
				Vector3 cameraSize = new Vector3 (rect.width, rect.height, 0f);
				position = Vector3.Scale(position, cameraSize);

				//finally correct world position if necessary
				if (worldSpace)
				{
					position = position + cameraComponent.transform.position + cameraDepthCorrection - (cameraSize/2);
				}

				return position;
			}

			//Prevents position from going outside of this camera's boundaries
			public Vector3 ClampPositionToViewport (Vector3 position)
			{
				return ClampPositionToRect(position, rect);
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
				UpdateRect();
			}

			public void OnDestroy ()
			{
				ControllerProvider.DisposeController<IViewportController>(this);
			}
		//ENDOF MonoBehaviour lifecycle implementation

		//private methods
			//updates the cached viewport for the active camera			
			protected void UpdateRect ()
			{
				rect = cameraComponent.EMRectFromOrthographicCamera();
			}


	//=============================================================================
		//[TO-DO] Move this elsewhere
		//=============================================================================
		//clamp a position within viewing range of Camera.main
		private Vector3 ClampPositionToRect (Vector3 position, Rect limitsRect)
		{
			return new Vector3
			(
				x: Mathf.Clamp(position.x, limitsRect.xMin, limitsRect.xMax),
				y: Mathf.Clamp(position.y, limitsRect.yMin, limitsRect.yMax),
				z: position.z
			);
		}
		//=============================================================================
		//[TO-DO] Move this elsewhere
	//=============================================================================
		//ENDOF private methods
	}
}