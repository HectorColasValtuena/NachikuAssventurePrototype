using UnityEngine;

namespace ASSpriteRigging.TailSystem
{
	public class TransformTailWiggleElement : MonoBehaviour, ITailElement
	{
	//Implementación ITailElement
		public float targetRotation
		{
			get { return _targetRotation; }
			set { _targetRotation = Mathf.Clamp(value, -maxOffsetRotation, maxOffsetRotation); }
		}
	//ENDOF Implementación ITailElement
		public float maxOffsetRotation = 30f;
		public bool baseRotationFromStartingRotation = true;

		private Quaternion baseRotation;
		private float _targetRotation = 0.0f;
		private float lerpRate = 0.001f;

		void Start()
		{
			baseRotation = baseRotationFromStartingRotation ? transform.rotation : Quaternion.identity;
		}

		void Update()
		{
			MatchRotation();
		}

		private void MatchRotation ()
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, baseRotation * Quaternion.Euler(0f, 0f, _targetRotation), lerpRate);
		}
	}
}