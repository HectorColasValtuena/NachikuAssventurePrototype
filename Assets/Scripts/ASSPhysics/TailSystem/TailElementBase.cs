//using System.Collections;
//	using System.Collections.Generic;

using UnityEngine;

using IPulsePropagator = ASSPhysics.PulseSystem.PulsePropagators.IPulsePropagator;

namespace ASSPhysics.TailSystem
{
	public abstract class TailElementBase : ASSPhysics.PulseSystem.PulsePropagators.ChainElementPulsePropagatorBase
	{
	//serialized/public fields and properties
		//absolute maximum rotation off from base rotation
		[SerializeField]
		private float _rotationMax;
		public float rotationMax { get { return _rotationMax; } set { _rotationMax = value; } }

		//soft rotation limit. pulse intensity value multiplies this value. can be exceeded if pulse > 1.0f
		[SerializeField]
		private float _rotationSoftLimit;
		public float rotationSoftLimit { get { return _rotationSoftLimit; } set { _rotationSoftLimit = value; } }

		/*
		//wether to fetch rotation from initial state
		[SerializeField]
		private bool _baseRotationFromStartingRotation;
		public bool baseRotationFromStartingRotation { get { return _baseRotationFromStartingRotation; } set { _baseRotationFromStartingRotation = value; } }
		*/
	//ENDOF serialized/public fields and properties

	//private fields and properties
		//protected Quaternion baseRotation;  //base rotation of the element. offsetRotation swings and is clamped around this value
	//ENDOF private fields and properties
		
	//MonoBehaviour lifecycle
		/*
		public virtual void Start ()
		{
			baseRotation = baseRotationFromStartingRotation ? transform.rotation : Quaternion.identity;
		}
		*/
		public virtual void FixedUpdate()
		{
			UpdateRotation(Time.fixedDeltaTime);
		}
	//ENDOF MonoBehaviour lifecycle

	//ChainElementPulsePropagatorBase abstract method implementation
		//get delay in seconds before propagation to target effectuates
		protected override float GetPropagationDelay (IPulsePropagator target)
		{
			return Vector3.Distance(transform.position, target.transform.position);
		}
	//ENDOF ChainElementPulsePropagatorBase abstract method implementation

	//Overridable methods
		//attempts to match current rotation with target rotation
		protected abstract void UpdateRotation (float timeDelta);
	//ENDOF Overridable methods
	}
}