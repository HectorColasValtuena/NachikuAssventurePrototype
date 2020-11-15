using UnityEngine;

using AnimationNames = ASSPhysics.Constants.AnimationNames;
using IInteractor = ASSPhysics.InteractableSystem.IInteractor;	//IInteractor
using EInputState = ASSPhysics.InputSystem.EInputState;


namespace ASSPhysics.HandSystem.Actions
{
	public class ActionUseInteractor : ActionBase
	{
	//ActionBase override implementation
		//receive state of corresponding input medium
		public override void Input (EInputState state)
		{
			//propagate input to the interactor
			//if interactor reports failure end the action
			if (!tool.interactor.Input(state))
			{
				Clear();
				return;
			}

			if (state == EInputState.Ended)
			{
				tool.SetAnimationState(AnimationNames.Tool.stateClickUp);
			}
			else if (state == EInputState.Started)
			{
				tool.SetAnimationState(AnimationNames.Tool.stateClickDown);
			}
		}

		//interaction is valid if hovering an interactable
		public override bool IsValid ()
		{
			return tool.interactor.IsHovering();
		}

		//Using an interactor is an entirely non-automatable one-shot action, so automation methods just report failure
		public override bool Automate () { return false; }
		public override bool AutomationUpdate () { return false; }
		//public override void DeAutomate ();

		//on clear ensure exiting animation state
		public override void Clear ()
		{
			tool.SetAnimationState(AnimationNames.Tool.stateClickUp);
			base.Clear();
		}
	//ENDOF ActionBase override implementation

	}
}
