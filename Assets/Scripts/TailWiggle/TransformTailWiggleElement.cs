using UnityEngine;

namespace SpriteRigging
{
	public class TransformTailWiggleElement : MonoBehaviour
	{
		private Quaternion initialRotation;

		public float offsetRotationTarget = 0.0f;
		private float lerpRate = 0.001f;
		public float maxOffsetRotation = 30.0f;

		void Start()
		{
			initialRotation = transform.rotation;
		}

		void Update()
		{
			MatchRotation();
		}

		private void MatchRotation ()
		{
			//float targetAngle = Mathf.SmoothStep(transform.rotation.eulerAngles.z, initialRotation + offsetRotationTarget, lerpRate);
			//transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, targetAngle);
			transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation * Quaternion.Euler(0f, 0f, offsetRotationTarget), lerpRate);
			//transform.rotation = initialRotation * Quaternion.Euler(0f, 0f, targetAngle);
		}

		public void RotateElement (float value)
		{
			offsetRotationTarget += value;
			offsetRotationTarget = Mathf.Clamp(offsetRotationTarget, -maxOffsetRotation, maxOffsetRotation);
		}
	}
}