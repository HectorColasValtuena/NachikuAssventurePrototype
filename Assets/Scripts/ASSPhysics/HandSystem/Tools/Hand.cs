using UnityEngine;

using InputSettings = ASSPhysics.SettingSystem.InputSettings; //InputSettings
using AnimationNames = ASSPhysics.Constants.AnimationNames;	//AnimationNames
using ASSPhysics.HandSystem.Actions; //IAction, ActionGrab, ActionUseInteractor

namespace ASSPhysics.HandSystem.Tools
{
	public class Hand : ToolBase
	{
	//MonoBehaviour Lifecycle implementation
/*
		public void Awake ()
		{
			base.Awake();
		//=============================================================================
		//[TO-DO]
		//=============================================================================
		}
		public void Update ()
		{
			base.Update();
		//=============================================================================
		//[TO-DO]
		//=============================================================================			
		}
*/
	//ENDOF MonoBehaviour Lifecycle implementation

	//ToolBase abstract implementation
		private float inputHeldTime = 0.0f;
		
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
				//SetAction<ActionSlap>();//InitiateSlap();
				Debug.Log("Slappin'");
			}
		}

		//sets the animator
		public override void SetAnimationState (string triggerName)
		{
			if (triggerName == null) { triggerName = AnimationNames.Tool.stateFlat; }
			base.SetAnimationState(triggerName);
		}
	//ENDOF ToolBase abstract implementation

	//private method implementation
		private void TryActions ()
		{
			if (SetAction<ActionUseInteractor>()) return;
			if (SetAction<ActionGrab>()) return;
		}
	}
}

