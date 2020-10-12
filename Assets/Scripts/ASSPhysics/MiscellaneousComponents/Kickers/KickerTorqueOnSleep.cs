using ASSistant.ASSRandom; //RandomRangeFloat

namespace ASSPhysics.MiscellaneousComponents.Kickers
{
	public class KickerTorqueOnSleep : KickerTorqueOnConditionBase
	{
	//abstract method implementation
		protected override bool CheckCondition ()
		{
			return targetRigidbody.IsSleeping();
		}
	//ENDOF abstract method implementation
	}
}