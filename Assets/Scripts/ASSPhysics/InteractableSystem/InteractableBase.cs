﻿using UnityEngine;
using UnityEngine.Events;	//UnityEvent

using AnimationNames = ASSPhysics.Constants.AnimationNames; 

using EInputState = ASSPhysics.InputSystem.EInputState; //EInputState

namespace ASSPhysics.InteractableSystem
{
	public abstract class InteractableBase : MonoBehaviour, IInteractable
	{
	//serialized fields and properties
		//callback stack to execute upon triggering
		[SerializeField]
		private UnityEvent callback = null;
	//serialized fields and properties

	//private fields and properties
		protected Animator animator;	//animator used by this interactable

		//wether this interactable is being highlighted by an active interactor
		private bool _highlighted = false;
		protected virtual bool highlighted
		{
			get { return _highlighted; }
			set
			{
				if (value != highlighted)
				{
					_highlighted = value;
					HighlightChanged(highlighted);
				}
			}
		}
	//ENDOF private fields and properties

	//IInteractable implementation
		//interactable with highest priority will be called when several in range
		[SerializeField]
		private int _priority = 0;
		public int priority { get { return _priority; }}

		public abstract void Interact (EInputState state);
	//ENDOF IInteractable implementation

	//MonoBehaviour lifecycle
		public virtual void Awake ()
		{
			animator = gameObject.GetComponent<Animator>();
		}

		//collision with an interactor highlights or un-highlights  this interactable
		public void OnTriggerEnter () { Debug.Log("TriggerEnter"); highlighted = true; }
		public void OnTriggerExit () { Debug.Log("TriggerExit"); highlighted = false; }
	//ENDOF MonoBehaviour lifecycle

	//private methods
		protected virtual void HighlightChanged (bool state)
		{
			if (animator != null)
				animator.SetBool(AnimationNames.Interactable.highlighted, state);
		}
		
		protected virtual void TriggerCallbacks () { callback.Invoke(); }
	//ENDOF private methods
	}
}