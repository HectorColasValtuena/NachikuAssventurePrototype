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
				transform.position = ClampPositionToCamera(value);
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

		//clamp a position within viewing range of Camera.main
		private Vector3 ClampPositionToCamera (Vector3 position)
		{
			return new Vector3
			(
				Mathf.Clamp
				(
					position.x,
					(-1 * Camera.main.orthographicSize * Camera.main.aspect) + Camera.main.transform.position.x,
					(Camera.main.orthographicSize * Camera.main.aspect) + Camera.main.transform.position.x
				),
				Mathf.Clamp
				(
					position.y,
					(-1 * Camera.main.orthographicSize) + Camera.main.transform.position.y,
					Camera.main.orthographicSize + Camera.main.transform.position.y
				),
				position.z
			);
		}

		protected abstract void InputStarted ();
		protected abstract void InputHeld ();
		protected abstract void InputEnded ();
	}
}