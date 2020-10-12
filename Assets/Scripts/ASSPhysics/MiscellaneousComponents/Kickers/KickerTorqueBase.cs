using UnityEngine;

namespace ASSPhysics.MiscellaneousComponents.Kickers
{
	public class KickerTorqueBase : MonoBehaviour, IKicker
	{
	//serialized properties 
		//Minimum and maximum force range for every kicker
		public float minForce;
		public float maxForce;

		public int direction; //if not zero determines the sign of the force applied. If 0, a direction will be chosen randomly each time

		public Rigidbody targetRigidbody; //will automatically get this gameobject's rigidbody on awake if none given
	//ENDOF serialized properties 

	//private fields and properties
	//ENDOF private fields and properties

	//IKicker implementation
		public void Kick ()
		{
			///////////////////////////////////////////////////////////////////////////////////////////////////////////
			//[TO-DO]
			
		}
	//ENDOF IKicker implementation

	//MonoBehaviour Lifecycle
		public void Awake ()
		{
			if (!targetRigidbody) targetRigidbody = GetComponent<Rigidbody>;
		}
	//ENDOF MonoBehaviour Lifecycle
	}
}