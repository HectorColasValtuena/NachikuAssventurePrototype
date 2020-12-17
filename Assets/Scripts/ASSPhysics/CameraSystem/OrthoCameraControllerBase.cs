using UnityEngine;

using ControllerProvider = ASSPhysics.ControllerSystem.ControllerProvider;

using RectMath = ASSistant.ASSMath.RectMath;
using static ASSPhysics.CameraSystem.CameraExtensions; //Camera.EMRectFromOrthographicCamera();

namespace ASSPhysics.CameraSystem
{
	public abstract class OrthoCameraControllerBase : MonoBehaviour, IViewportController
	{
	//serialized fields
		[SerializeField]
		protected Camera cameraComponent; //cached reference to the camera this controller handles

		[SerializeField]
		protected Vector3 cameraDepthCorrection = new Vector3 (0f, 0f, 10f); //camera Z depth correction
	//ENDOF serialized fields

	//private fields
		protected Vector3 transformPosition
		{
			get { return cameraComponent.transform.position; }
			set 
			{
				cameraComponent.transform.position = value;
				UpdateRect();
			}
		}

		protected float cameraSize 
		{
			get { return cameraComponent.orthographicSize; }
			set
			{
				cameraComponent.orthographicSize = value; 
				UpdateRect();
			}
		}
	//ENDOF private fields

	//IViewportController implementation
		public virtual Rect rect { get; protected set; } //current size of the viewport

		public virtual float size //current height value of the viewport
		{
			get { return cameraSize; }
			set { cameraSize = value; }
		}

		public virtual Vector3 position
		{
			get { return transformPosition; }
			set { transformPosition = value; }
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
			return RectMath.ClampPositionToRect(position, rect);
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

	//private class methods
		//updates the cached viewport for the active camera			
		protected void UpdateRect ()
		{
			rect = cameraComponent.EMRectFromOrthographicCamera();
		}
	//ENDOF private methods
	}
}