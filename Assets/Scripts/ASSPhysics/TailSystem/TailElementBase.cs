using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace ASSPhysics.TailSystem
{
	public abstract class TailElementBase : MonoBehaviour, IPulsePropagator
	{
	//serialized/public fields and properties
		public float maxOffsetRotation = 30f;		//maximum rotation off from the base value
		public bool baseRotationFromStartingRotation = true;	//wether to fetch rotation from initial state
	//ENDOF serialized fields and properties

	//private fields and properties
		protected Quaternion baseRotation;  //base rotation of the element. offsetRotation swings and is clamped around this value
	//ENDOF private fields and properties

	//implementación IPulsePropagator
		public void Pulse (
			float pulseIntensity,			//intensity for the effects of the pulse. default 1
			int pulseSign,					//direction of the effects of the pulse. 1 positive -1 negative 0 default/random. default 0
			float propagationDelayModifier,	//modifier for pulse propagation time delay. default 1
			int propagationDirection 		//propagation direction. 1 towards children, -1 towards parent, 0 default. default 0
		) {
			///////////////////////////////////////////////////////////////////////////////////////////////////////////////
		}
	//ENDOF implementación IPulsePropagator
		
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