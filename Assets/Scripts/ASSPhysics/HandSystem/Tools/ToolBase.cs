﻿using UnityEngine;

using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

using ASSPhysics.HandSystem.Actions; //IAction
using AnimationNames = ASSPhysics.Constants.AnimationNames;
using EInputState = ASSPhysics.InputSystem.EInputState;
using IInteractor = ASSPhysics.InteractableSystem.IInteractor;

namespace ASSPhysics.HandSystem.Tools
{
	//[RequireComponent(typeof(Animator))]
	public abstract class ToolBase : MonoBehaviour, ITool
	{
	//Local variables
		//cached reference to animator component
		[SerializeField]
		protected Animator animator;
		//current action
		protected IAction action = null;
	//ENDOF Local variables

	//MonoBehaviour lifecycle Implementation
		public virtual void Awake ()
		{
			if (animator == null) { animator = gameObject.GetComponentInChildren<Animator>(); }
			interactor = GetComponentInChildren<IInteractor>();
		}

		public virtual void Update ()
		{
			if (auto)
			{
				action.AutomationUpdate();
			}
		}
	//MonoBehaviour LifeCycle Implementation

	//ITool implementation
		//is this tool in auto mode
		private bool _auto = false;
		public bool auto
		{
			get { return _auto;	}
			protected set
			{
				_auto = value;
				animator.SetBool(AnimationNames.Tool.automated, value);
			}
		}

		//private IInteractor _interactor;
		public IInteractor interactor {get; private set;}

		public IAction activeAction { get { return action; } }

		public Vector3 position
		{
			set 
			{
				if (auto) return;
				transform.position = ControllerCache.viewportController.ClampPositionToViewport(value);
			}
			get { return transform.position; }
		}

		//wether the hand is on focus or not
		private bool _focused = false;
		public bool focused
		{
			get { return _focused; }
			set
			{
				if (_focused == true && value == false) { LostFocus(); }
				_focused = value;
				animator.SetBool(AnimationNames.Tool.focused, value);
			}
		}

		//move the tool in worldspace
		public void Move (Vector3 delta)
		{
			position = position + delta;
		}

		//Main Input Receiver
		//called upon pressing, holding, and releasing input button
		//propagates input to action only if non-automated. If automated, exits automation on input started
		public void MainInput (EInputState state, Vector3 targetPosition)
		{
			position = targetPosition;
			MainInput (state);
		}
		public void MainInput (EInputState state)
		{
			//if automated ignore input save for determining wether to finish or not
			if (auto)
			{
				if (state == EInputState.Started)
				{
					DeAutomate();
				}
				return;
			}
			
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

		//sets the animator
		public virtual void SetAnimationState (string triggerName)
		{
			animator.SetTrigger(triggerName);
		}
	//ENDOF ITool implementation

	//Private functionality
		//Start an action of type T unless its the type currently active
		//the initialize it with a reference to ourselves and return its startup validity check
		protected bool SetAction <T> () where T : class, IAction, new()
		{
			//if the current action is NOT of the same type as the target
			//only then attempt to create a new action
			if ((action as T) == null)
			{
				//create the new action
				IAction newAction = new T ();
				//initialize it with a proper reference and check if it's valid
				if (newAction.Initialize((ITool)this))
				{
					action?.Clear(); //call Clear on the previous action for cleanup
					action = newAction; //store the new action
					return true;	//return true indicating valid action
				}

				return false;	//return false indicating failed action
			}
			//if the current action IS of the target type, re-initialize it and return its validity check
			return action.IsValid();
		}

		//called when losing focus
		//when being unfocused the tool tries to automate the ongoing action
		protected void LostFocus ()
		{
			if (!auto)
			{
				Automate();
			}
		}

		//attempt to set in automated mode. 
		protected void Automate ()
		{
			if (action != null && !action.auto)
			{
				auto = action.Automate();
				Debug.Log("automating tool > " + auto);
			}
		}

		//finish auto mode
		protected void DeAutomate ()
		{
			if (action != null && action.auto)
			{
				action.DeAutomate();
			}
			auto = false;
		}
	//ENDOF Private functionality

	//Protected abstract method exposed for implementation
		protected abstract void InputStarted ();
		protected abstract void InputHeld ();
		protected abstract void InputEnded ();
	//ENDOF Protected abstract method exposed for implementation
	}
}