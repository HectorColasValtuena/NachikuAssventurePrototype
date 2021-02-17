using UnityEngine;

namespace ASSPhysics.MiscellaneousComponents.Kickers
{
	public abstract class OnOffFlickerManualActivationBase : OnOffFlickerBase
	{
	//inherited abstract method implementation
		protected override bool CheckCondition ()
		{ return false; }
	//ENDOF inherited abstract method implementation
	}
}