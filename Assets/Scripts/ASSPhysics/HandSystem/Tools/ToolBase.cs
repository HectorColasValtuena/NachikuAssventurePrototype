using UnityEngine;

using AssPhysics.Constants;
using ASSPhysics.HandSystem.Actions; //IAction

namespace ASSPhysics.HandSystem.Tools
{
	[RequireComponent(typeof(Animator))]
	public abstract class ToolBase : MonoBehaviour, ITool
	{
	//Object initialization and local variables
		protected Animator animator;
		protected IAction action;

		public virtual void Awake () {
			animator = gameObject.GetComponent<Animator>();
		}

	//ENDOF Object initialization and local variables

	//ITool implementation
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
			else { InputEnded(); } //EInputState.Ended:

			if (action != null)
			{
				action.Input(state);
			}
		}

		//Called by the current action to remove itself
		public void ActionEnded ()
		{
			action = null;
		}
	//ENDOF IHand implementation

	//Private functionality
		//ensure current action matches with type T
		protected void SetAction <T> () where T : class, IAction, new()
		{
			if ((action as T) == null)
			{
				action?.Clear();
				action = new T ();
			}
		}

		//=============================================================================
		//[TO-DO] Move this elsewhere
		//=============================================================================
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
		//=============================================================================
		//[TO-DO] Move this elsewhere
		//=============================================================================
	//ENDOF Private functionality

	//Protected abstract method exposed for implementation
		protected abstract void InputStarted ();
		protected abstract void InputHeld ();
		protected abstract void InputEnded ();
	//ENDOF Protected abstract method exposed for implementation
	}
}