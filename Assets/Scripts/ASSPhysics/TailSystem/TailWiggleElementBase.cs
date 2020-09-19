using UnityEngine;

namespace ASSPhysics.TailSystem
{
	public abstract class TailWiggleElementBase : MonoBehaviour, ITailElement
	{
	//Implementación ITailElement
		public float targetRotation
		{
			get { return _targetRotation; }
			set { _targetRotation = Mathf.Clamp(value, -maxOffsetRotation, maxOffsetRotation); }
		}
		private float _targetRotation = 0.0f;
	//ENDOF Implementación ITailElement
		public float maxOffsetRotation = 30f;
		public bool baseRotationFromStartingRotation = true;

		//base rotation of the element. Target rotation swings and is clamped around this value
		protected Quaternion baseRotation;

		void Start()
		{
			baseRotation = baseRotationFromStartingRotation ? transform.rotation : Quaternion.identity;
		}

		void Update()
		{
			MatchRotation();
		}

		//attempts to match current rotation with target rotation
		protected abstract void MatchRotation ();
	}
}