﻿using UnityEngine;

using RectMath = ASSistant.ASSMath.RectMath;

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
				targetPosition = RectMath.ClampPositionToRect(position: value, outerRect: containerRect);
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
			UpdateMoveTowardsTarget();
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

		protected virtual void UpdateMoveTowardsTarget ()
		{
			transformPosition = Vector3.Lerp(a: transformPosition, b: targetPosition, t: lerpRate);
		}

		private void ClampViewportToContainer ()
		{
			Vector3 newPosition = RectMath.ClampRectPositionToRect(innerRect: rect, outerRect: containerRect).center;
			newPosition -= cameraDepthCorrection;
			transformPosition = newPosition;
		}
	//ENDOF private methods
	}
}