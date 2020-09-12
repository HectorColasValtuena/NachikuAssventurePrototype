using UnityEngine;

namespace ASSPhysics.HandSystem.Tools
{
	public abstract class ToolBase : MonoBehaviour, ITool
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

		protected abstract void InputStarted ();
		protected abstract void InputHeld ();
		protected abstract void InputEnded ();
	}
}