using UnityEngine;

using ASSistant.ASSRandom; //RandomRangeFloat

namespace ASSPhysics.MiscellaneousComponents.Kickers
{
	public abstract class KickerOnRigidbodySleepBase : KickerOnConditionForceBase
	{
	//abstract method implementation
		protected override bool CheckCondition ()
		{
			return targetRigidbody.IsSleeping();
		}
	//ENDOF abstract method implementation
	}
}