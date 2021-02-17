using UnityEngine;

namespace ASSPhysics.MiscellaneousComponents.Kickers
{
	public abstract class OnOffFlickerAutoFireBase : OnOffFlickerBase
	{
	//inherited abstract method implementation
		protected override bool CheckCondition ()
		{ return !flickIsUp; }
	//ENDOF inherited abstract method implementation
	}
}