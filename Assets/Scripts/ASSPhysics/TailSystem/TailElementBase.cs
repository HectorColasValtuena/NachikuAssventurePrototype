using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using ASSPhysics.PulseSystem;

namespace ASSPhysics.TailSystem
{
	public abstract class TailElementBase : ChainElementPulsePropagatorBase
	{
	//serialized/public fields and properties
		public float maxOffsetRotation = 30f;		//maximum rotation off from the base value
		public bool baseRotationFromStartingRotation = true;	//wether to fetch rotation from initial state
	//ENDOF serialized fields and properties

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

	//Overridable methods
		//attempts to match current rotation with target rotation
		protected abstract void MatchRotation ();
	//ENDOF Overridable methods
	}
}