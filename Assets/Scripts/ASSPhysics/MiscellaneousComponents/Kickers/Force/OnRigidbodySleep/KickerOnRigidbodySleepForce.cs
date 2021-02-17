using UnityEngine;

using Vector3Math = ASSistant.ASSMath.Vector3Math;
using RandomRangeFloat = ASSistant.ASSRandom.RandomRangeFloat;

namespace ASSPhysics.MiscellaneousComponents.Kickers
{
	public class KickerOnRigidbodySleepForce : KickerOnRigidbodySleepBase
	{
	//serialized properties
		[SerializeField]
		public RandomRangeFloat forceAngleRange;
	//ENDOF serialized properties 

	//IKicker implementation
		//applies a random force at a random direction as the kick
		public override void Kick ()
		{
			targetRigidbody.AddForce(
					force: Vector3Math.AngleToVector3(forceAngleRange.Generate()) * randomForce.Generate(),
					mode: ForceMode.Force
				);
		}
	//ENDOF IKicker implementation
	}
}