using UnityEngine;

namespace ASSPhysics.CameraSystem
{
	public class OrthoCameraControllerScrollable : OrthoCameraControllerBase
	{
	//serialized fields
		[SerializeField]
		private Rect containerRect; //current size of the viewport
		[SerializeField]
		private bool autoConfigureLimits = true;
		[SerializeField]
		private float lerpRate = 0.05f;
	//ENDOF serialized fields

	//base class overrides
		//this extending class' position uses an intermediary target position for position lerping
		public override Vector3 position
		{
			get { return base.position; }
			set 
			{
				targetPosition = ClampPositionToRect(position: value, outerRect: containerRect);
			}
		}
	//ENDOF base class overrides

	//private fields and properties
		private Vector3 targetPosition;
	//ENDOF private fields and properties

	//MonoBehaviour Lifecycle
		public override void Start ()
		{
			base.Start();
			targetPosition = position;
			if (autoConfigureLimits) { ConfigureLimitsFromCameraSize(); }
		}

		public virtual void Update ()
		{
			UpdateLerpPositionToTarget();
		}

		public void LateUpdate ()
		{
			ClampViewportToContainer();
		}
	//ENDOF MonoBehaviour Lifecycle

	//private methods
		private void ConfigureLimitsFromCameraSize ()
		{
			containerRect = cameraComponent.EMRectFromOrthographicCamera();
		}

		private void UpdateLerpPositionToTarget ()
		{
			transformPosition = Vector3.Lerp(a: transformPosition, b: targetPosition, t: lerpRate);
		}

		private void ClampViewportToContainer ()
		{
			Vector3 newPosition = ClampRectPositionToRect(innerRect: rect, outerRect: containerRect).center;
			newPosition.z = transformPosition.z;
			transformPosition = newPosition;
		}
	//ENDOF private methods
	}
}