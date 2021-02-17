using UnityEngine;

using ASSistant.ASSRandom; //RandomRangeFloat

namespace ASSPhysics.MiscellaneousComponents.Kickers
{
	public class KickerOnRigidbodySleepTorque : KickerOnRigidbodySleepBase
	{
	//serialized properties 
		public int direction; //if not zero determines the sign of the force applied. If 0, a direction will be chosen randomly each time
	//ENDOF serialized properties 

	//private fields and properties
	//ENDOF private fields and properties

	//IKicker implementation
		//applies a random torque at a random direction as the kick
		public override void Kick ()
		{
			//add a torque of random intensity and random direction in Z axis
			targetRigidbody.AddTorque(
				Vector3.forward * randomForce.Generate() * GetDirection(),
				ForceMode.Force
			);
		}
	//ENDOF IKicker implementation

	//MonoBehaviour Lifecycle
	//ENDOF MonoBehaviour Lifecycle

	//private methods
		private int GetDirection ()
		{
			return (direction > 0)
						? 1			//if direction sign is + use 1
					 : (direction < 0)
						? -1		//if direction sign is - use -1
						: RandomSign.Generate();	//if none, get a random sign
		}
	//ENDOF private methods
	}
}