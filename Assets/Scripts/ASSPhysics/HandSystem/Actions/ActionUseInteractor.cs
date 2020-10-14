using UnityEngine;

using IInteractor = ASSPhysics.InteractableSystem.IInteractor;	//IInteractor
using EInputState = ASSPhysics.InputSystem.EInputState;

namespace ASSPhysics.HandSystem.Actions
{
	public class ActionUseInteractor : ActionBase
	{
	//ActionBase override implementation
		//returns true if this action is currently doing something, like maintaining a grab or repeating a slapping pattern
		//Will be true if base.ongoing (because automated) or if we have a joint list acting upon the world
		//receive state of corresponding input medium
		public override void Input (EInputState state)
		{
			if (state == EInputState.Started)
			{
				InitiateGrab();
			}
			if (state == EInputState.Ended)
			{
				FinishGrab();
			}
		}

		//returns true if the action can be legally activated at its position
		public override bool IsValid ()
		{
/////////////////////////////////////////////////////////////////////////////////////////////////////
//[TO-DO] optimize initialization by keeping a copy of bone list?
//[TO-DO] consider OverlapCircleNonAlloc for fast validity checks too
/////////////////////////////////////////////////////////////////////////////////////////////////////			
			return (GetBoneCollidersInRange().Length > 0);
		}

		//Using an interactor is an entirely non-automatable one-shot action, so automation methods just report failure
		public override bool Automate () { return false; }
		public override bool AutomationUpdate () { return false; }
		//public override void DeAutomate ();
	//ENDOF ActionBase override implementation

	}
}
