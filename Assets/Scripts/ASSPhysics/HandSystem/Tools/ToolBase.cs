using UnityEngine;

using AssPhysics.Constants;

namespace ASSPhysics.HandSystem.Tools
{
	[RequireComponent(typeof(Animator))]
	public abstract class ToolBase : MonoBehaviour, ITool
	{
	//Object initialization and local variables
		protected Animator animator;

		public virtual void Awake () {
			animator = gameObject.GetComponent<Animator>();
		}

	//ENDOF Object initialization and local variables

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

		//wether the hand is on focus or not
		private bool _focused;
		public bool focused
		{
			get { return _focused; }
			set
			{
				Debug.Log(animator);
				_focused = value;
				animator.SetBool(AnimationNames.focused, value);
			}
		}

		public void Move (Vector3 delta)
		{
			position = position + delta;
		}
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

	//Private functionality
		//clamp a position within viewing range of Camera.main
		private Vector3 ClampPositionToCamera (Vector3 position)
		{
		//=============================================================================
		//[TO-DO] Move this elsewhere
		//=============================================================================
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
	//ENDOF Private functionality

		protected abstract void InputStarted ();
		protected abstract void InputHeld ();
		protected abstract void InputEnded ();
	}
}