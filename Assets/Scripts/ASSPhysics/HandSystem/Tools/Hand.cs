using UnityEngine;
using ASSPhysics.Settings; //InputSettings
using ASSPhysics.Constants;	//AnimationNames
using ASSPhysics.HandSystem.Actions; //ActionGrab

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
			if (!TrySpecialAction())
			{
				InitiateGrab();
			}
		}

		protected override void InputHeld ()
		{
			inputHeldTime += Time.deltaTime;
		}

		protected override void InputEnded ()
		{
			if (inputHeldTime <= InputSettings.maximumTimeHeldForSlap)
			{
				InitiateSlap();
			}
		}

		//sets the animator
		public override void SetAnimationState (string triggerName)
		{
			if (triggerName == null) { triggerName = AnimationNames.Tool.stateFlat; }
			base.SetAnimationState(triggerName);
		}
	//ENDOF ToolBase abstract implementation



		//Checks if we have to perform a special action and initiate it
		//returns true if we have a special action to perform
		private bool TrySpecialAction ()
		{
		//=============================================================================
		//[TO-DO] temporary version always returns false
		//=============================================================================
			return false;
		}

		private void InitiateGrab ()
		{
			Debug.Log("InitiateGrab();");
			SetAction<ActionGrab>();
		}

		private void InitiateSlap ()
		{
			Debug.Log("InitiateSlap();");
		//=============================================================================
		//[TO-DO]
		//=============================================================================
		}

		//private void EndActions ()
		//{}
	}
}

