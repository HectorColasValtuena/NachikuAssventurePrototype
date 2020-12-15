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
	//ENDOF private fields

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
			get { return transformPosition; }
			set 
			{
				transformPosition = value;
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

	//private class methods
		//updates the cached viewport for the active camera			
		protected void UpdateRect ()
		{
			rect = cameraComponent.EMRectFromOrthographicCamera();
		}

	//=============================================================================
	  //[TO-DO] Move this elsewhere
	  //=============================================================================
		//clamp a x/y position within a rect
		protected Vector3 ClampPositionToRect (Vector3 position, Rect outerRect)
		{
			return new Vector3
			(
				x: Mathf.Clamp(position.x, outerRect.xMin, outerRect.xMax),
				y: Mathf.Clamp(position.y, outerRect.yMin, outerRect.yMax),
				z: position.z
			);
		}

		//ensures innerRect bounds stay within outerRect by moving innerRect if protruding.
		//if innerRect dimensions exceed outerRect, they will be centered
		protected Rect ClampRectPositionToRect (Rect innerRect, Rect outerRect)
		{
			return new Rect (
				x: (innerRect.width <= outerRect.width)
					? //if innerRect is thinner than outerRect, clamp its position within outerRect
						Mathf.Clamp(					
							value: innerRect.x,
							min: outerRect.xMin,
							max: outerRect.xMax - innerRect.width
						)
					: //if innerRect is wider than outerRect, center their position
						outerRect.x - ((innerRect.width - outerRect.width) / 2),
				y: (innerRect.height <= outerRect.height)
					? //if innerRect is shorter than outerRect clamp its position
						Mathf.Clamp(
							value: innerRect.y,
							min: outerRect.yMin,
							max: outerRect.yMax - innerRect.height
						)
					: //if innerRect is taller than outerRect, center their position
						innerRect.y - ((innerRect.height - outerRect.width) / 2),
				width: innerRect.width,
				height: innerRect.height
			);
		}

		//truncates innerRect dimensions to fit outerRect. may return the same rect if already small enough.
		//only alters size, returned rect's position will be the same as innerRect's
		protected Rect ClampRectSizeToRect (Rect innerRect, Rect outerRect)
		{
			if (innerRect.width <= outerRect.width && innerRect.height <= outerRect.height)
			{ return innerRect; }
			return new Rect (
				x: innerRect.x,
				y: innerRect.y,
				width: Mathf.Clamp(innerRect.width, 0, outerRect.width),
				height: Mathf.Clamp(innerRect.height, 0, outerRect.height)
			);
		}

	  //=============================================================================
	  //[TO-DO] Move this elsewhere
	//=============================================================================
		
	//ENDOF private methods
	}
}