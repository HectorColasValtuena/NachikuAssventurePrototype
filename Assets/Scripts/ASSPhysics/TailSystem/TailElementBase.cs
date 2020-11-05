//using System.Collections;
//	using System.Collections.Generic;

using UnityEngine;

using IPulsePropagator = ASSPhysics.PulseSystem.PulsePropagators.IPulsePropagator;

namespace ASSPhysics.TailSystem
{
	public abstract class TailElementBase : ASSPhysics.PulseSystem.PulsePropagators.ChainElementPulsePropagatorBase
	{
	//serialized/public fields and properties
		public float rotationMax = 30f;			//absolute maximum rotation off from base rotation
		public float rotationSoftLimit = 20f;	//soft rotation limit. pulses of value 1.0f will result in this. can be exceeded
		public bool baseRotationFromStartingRotation = true;	//wether to fetch rotation from initial state
	//ENDOF serialized/public fields and properties

	//private fields and properties
		protected Quaternion baseRotation;  //base rotation of the element. offsetRotation swings and is clamped around this value
	//ENDOF private fields and properties
		
	//MonoBehaviour lifecycle
		public virtual void Start ()
		{
			baseRotation = baseRotationFromStartingRotation ? transform.rotation : Quaternion.identity;
		}

		public virtual void Update()
		{
			MatchRotation();
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
		protected abstract void MatchRotation ();
	//ENDOF Overridable methods
	}
}