using UnityEngine;

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
			if (inputHeldTime <= maximumTimeHeldForSlap)
			{
				Slap();
			}
			EndActions();
		}
	//ENDOF ToolBase abstract implementation

		//private const float minimumTimeHeldForGrab = 0.1f;
		private const float maximumTimeHeldForSlap = 0.1f;
		private float inputHeldTime = 0.0f;


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
		//=============================================================================
		//[TO-DO]
		//=============================================================================
		}

		private void Slap ()
		{
		//=============================================================================
		//[TO-DO]
		//=============================================================================
		}

		private void EndActions ()
		{}
	}
}

