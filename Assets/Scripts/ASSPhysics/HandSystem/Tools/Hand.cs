using UnityEngine;

using InputSettings = ASSPhysics.SettingSystem.InputSettings;
using AnimationNames = ASSPhysics.Constants.AnimationNames;
using ASSPhysics.HandSystem.Actions; //IAction, ActionGrab, ActionSlap ActionUseInteractor

namespace ASSPhysics.HandSystem.Tools
{
	public class Hand : ToolBase
	{
	//private fields and properties
		private float inputHeldTime = 0.0f;
	//ENDOF private fields and proerties

	//MonoBehaviour Lifecycle implementation
	//ENDOF MonoBehaviour Lifecycle implementation
		
	//ToolBase implementation
		protected override void InputStarted ()
		{
			//upon first starting an input, try to determine if an special zone action is required
			//if not, try to initiate a grab.
			inputHeldTime = 0.0f;
			TryActions();
		}

		protected override void InputHeld ()
		{
			inputHeldTime += Time.deltaTime;
		}

		protected override void InputEnded ()
		{
			if (inputHeldTime <= InputSettings.maximumTimeHeldForSlap)
			{
				SetAction<ActionSlap>();//InitiateSlap();
				Debug.Log("Slappin'");
			}
		}

		//sets the animator
		public override void SetAnimationState (string triggerName)
		{
			if (triggerName == null) { triggerName = AnimationNames.Tool.stateFlat; }
			base.SetAnimationState(triggerName);
		}
	//ENDOF ToolBase implementation

	//private method implementation
		private void TryActions ()
		{
			if (SetAction<ActionUseInteractor>()) return;
			if (SetAction<ActionGrab>()) return;
		}
	//ENDOF private method implementation
	}
}

