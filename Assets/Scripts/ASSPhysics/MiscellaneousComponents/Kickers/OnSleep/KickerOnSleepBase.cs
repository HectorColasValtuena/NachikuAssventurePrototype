using UnityEngine;

using ASSistant.ASSRandom; //RandomRangeFloat

namespace ASSPhysics.MiscellaneousComponents.Kickers
{
	public abstract class KickerOnSleepBase : KickerOnConditionBase
	{
	//abstract method implementation
		protected override bool CheckCondition ()
		{
			return targetRigidbody.IsSleeping();
		}
	//ENDOF abstract method implementation
	}
}