using UnityEngine;

namespace ASSPhysics.HandSystem
{
	public class Hand : MonoBehaviour, ITool
	{
	//IHand implementation
		public Vector3 position
		{
			set 
			{
				//[TO-DO] ignore set if hand is in auto mode
				transform.position = value;
			}
			get { return transform.position; }
		}

		public bool focused {get; set;}		//wether the hand is on focus or not

		public void Move (Vector3 delta)
		{
			position = position + delta;
		}
		//=============================================================================
		//[TO-DO]
		//=============================================================================
		//called upon pressing, holding, and releasing input button
		public void MainInput (EInputState state, Vector3 targetPosition)
		{
			position = targetPosition;
			MainInput (state);
		}
		public void MainInput (EInputState state)
		{
			if (state == EInputState.Held) { InputHeld(); }
			else if (state == EInputState.Started) { InputStarted(); }
			else /*EInputState.Ended:*/	{ InputEnded(); }
		}
	//ENDOF IHand implementation

	//MonoBehaviour Lifecycle implementation
		public void Awake ()
		{
		//=============================================================================
		//[TO-DO]
		//=============================================================================
		}

		public void Update()
		{
		//=============================================================================
		//[TO-DO]
		//=============================================================================			
		}
	//ENDOF MonoBehaviour Lifecycle implementation

		//private const float minimumTimeHeldForGrab = 0.1f;
		private const float maximumTimeHeldForSlap = 0.1f;
		private float inputHeldTime = 0.0f;

		//upon first starting an input, try to determine if an special zone action is required. if not, try to initiate a grab.
		private void InputStarted ()
		{
			inputHeldTime = 0.0f;
			if (!TrySpecialAction())
			{
				InitiateGrab();
			}
		}

		private void InputHeld ()
		{
			inputHeldTime += Time.deltaTime;
		}

		private void InputEnded ()
		{
			if (inputHeldTime <= maximumTimeHeldForSlap)
			{
				Slap();
			}
			EndActions();
		}

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

