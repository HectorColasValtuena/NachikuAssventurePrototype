using UnityEngine;

using ASSistant.ASSRandom; //RandomRangeFloat

namespace ASSPhysics.MiscellaneousComponents.Kickers
{
	public class KickerTorqueBase : MonoBehaviour, IKicker
	{
	//serialized properties 
		//Minimum and maximum force range for every kicker
		public RandomRangeFloat randomForce;

		public int direction; //if not zero determines the sign of the force applied. If 0, a direction will be chosen randomly each time

		public Rigidbody targetRigidbody; //will automatically get this gameobject's rigidbody on awake if none given
	//ENDOF serialized properties 

	//private fields and properties
	//ENDOF private fields and properties

	//IKicker implementation
		//applies a random torque at a random direction as the kick
		public void Kick ()
		{
			//turn direction
			int sign = (direction > 0)
						? 1			//if direction sign is + use 1
					 : (direction < 0)
						? -1		//if direction sign is - use -1
						: RandomSign.Generate();	//if none, get a random sign

			//add a torque of random intensity and random direction in Z axis
			targetRigidbody.AddTorque(
				Vector3.forward * randomForce.Generate() * RandomSign.Generate(),
				ForceMode.Force
			);
		}
	//ENDOF IKicker implementation

	//MonoBehaviour Lifecycle
		public void Awake ()
		{
			if (!targetRigidbody) targetRigidbody = gameObject.GetComponent<Rigidbody>();
		}
	//ENDOF MonoBehaviour Lifecycle
	}
}