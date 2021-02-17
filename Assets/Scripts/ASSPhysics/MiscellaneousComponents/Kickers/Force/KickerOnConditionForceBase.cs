using UnityEngine;

using ASSistant.ASSRandom; //RandomRangeFloat

namespace ASSPhysics.MiscellaneousComponents.Kickers
{
	public abstract class KickerOnConditionForceBase : KickerOnConditionHeldOnFixedUpdateBase
	{
	//serialized properties 
		//Minimum and maximum force range for every kicker
		public RandomRangeFloat randomForce;

		public Rigidbody targetRigidbody; //will automatically get this gameobject's rigidbody on awake if none given
	//ENDOF serialized properties 

	//private fields and properties
	//ENDOF private fields and properties

	//MonoBehaviour Lifecycle
		public void Awake ()
		{
			if (!targetRigidbody) targetRigidbody = gameObject.GetComponent<Rigidbody>();
		}
	//ENDOF MonoBehaviour Lifecycle
	}
}