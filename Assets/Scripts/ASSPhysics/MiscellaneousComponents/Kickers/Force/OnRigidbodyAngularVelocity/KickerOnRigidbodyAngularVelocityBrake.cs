using UnityEngine;

using Vector3Math = ASSistant.ASSMath.Vector3Math;
using RandomRangeFloat = ASSistant.ASSRandom.RandomRangeFloat;

namespace ASSPhysics.MiscellaneousComponents.Kickers
{
	public class KickerOnRigidbodyAngularVelocityBrake : KickerOnRigidbodyAngularVelocityBase
	{
	//serialized properties
		[SerializeField]
		private float brakingRatio = 0.5f;
	//ENDOF serialized properties 

	//IKicker implementation
		//dampens current speed
		public override void Kick ()
		{
			Debug.Log("Braking");
			targetRigidbody.AddTorque(
					torque: targetRigidbody.angularVelocity * -1 * brakingRatio,
					mode: ForceMode.VelocityChange
				);
		}
	//ENDOF IKicker implementation
	}
}