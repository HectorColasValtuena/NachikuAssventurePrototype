using UnityEngine;

using RandomRangeFloat = ASSistant.ASSRandom.RandomRangeFloat; //RandomRangeFloat

namespace ASSPhysics.MiscellaneousComponents.Kickers
{
	public class KickerOnRigidbodySleepForce : KickerOnRigidbodySleepBase
	{
	//serialized properties 
		public RandomRangeFloat forceAngleRange;
	//ENDOF serialized properties 

	//private fields and properties
	//ENDOF private fields and properties

	//IKicker implementation
		//applies a random torque at a random direction as the kick
		public override void Kick ()
		{
			targetRigidbody.AddForce(
					force: AngleToVector3(forceAngleRange.Generate()) * randomForce.Generate(),
					mode: ForceMode.Force
				);
		}
	//ENDOF IKicker implementation

	//MonoBehaviour Lifecycle
	//ENDOF MonoBehaviour Lifecycle

	//private methods
		private Vector3 AngleToVector3 (float angle)
		{
			return new Vector3 (Mathf.Sin(angle), Mathf.Cos(angle), 0);
		}
	//ENDOF private methods
	}
}