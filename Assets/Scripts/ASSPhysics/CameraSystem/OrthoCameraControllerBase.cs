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
	//ENDOF serialized fields

	//private fields
		protected Vector2 transformPosition
		{
			get { return (Vector2) cameraComponent.transform.position; }
			set 
			{
				//when setting position, add current Z position to target vector2 position
				cameraComponent.transform.position =
					(Vector3) value	+
					Vector3.Scale(cameraComponent.transform.position, Vector3.forward);
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

		public virtual Vector2 position
		{
			get { return transformPosition; }
			set { transformPosition = value; }
		}

		//transforms a screen point into a world position
		//if worldSpace is false, the returned Vector3 ignores camera transform position
		public Vector2 ScreenSpaceToWorldSpace (
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
				screenPosition = screenPosition + transformPosition - (cameraSize/2);
			}

			return screenPosition;
		}
		public Vector3 ScreenSpaceToWorldSpace (
			Vector3 screenPosition,
			bool worldSpace
		) {
			Vector2 vector2Pos = ScreenSpaceToWorldSpace((Vector2) screenPosition, worldSpace);
			return new Vector3 (vector2Pos.x, vector2Pos.y, screenPosition.z);
		}

		//Prevents position from going outside of this camera's boundaries
		public Vector2 ClampPositionToViewport (Vector2 position)
		{ return RectMath.ClampVector2WithinRect(position, rect); }
		public Vector3 ClampPositionToViewport (Vector3 position)
		{ return RectMath.ClampVector3WithinRect(position, rect); }
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