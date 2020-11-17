using UnityEngine;

using ASSistant.ASSRandom; //RandomRangeFloat

namespace ASSPhysics.MiscellaneousComponents.Kickers
{
	public abstract class KickerAutoFireBase : KickerOnConditionBase
	{
	//abstract method implementation
		protected override bool CheckCondition ()
		{
			return true;
		}
	//ENDOF abstract method implementation
	}
}