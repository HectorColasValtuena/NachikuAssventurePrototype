using UnityEngine;
using UnityEngine.Events;	//UnityEvent

using AnimationNames = ASSPhysics.Constants.AnimationNames; 

using EInputState = ASSPhysics.InputSystem.EInputState; //EInputState

namespace ASSPhysics.InteractableSystem
{
	public abstract class InteractableBase : MonoBehaviour, IInteractable
	{
	//serialized fields and properties
		public UnityEvent callback;
	//serialized fields and properties

	//private fields and properties
		protected Animator animator;	//animator used by this interactable

		//wether this interactable is being highlighted by an active interactor
		private bool _highlighted = false;
		protected bool highlighted
		{
			get { return _highlighted; }
			set
			{
				if (value != _highlighted)
				{
					_highlighted = value;
					animator.SetBool(AnimationNames.Interactable.highlighted, value);
				}
			}
		}
	//ENDOF private fields and properties

	//IInteractable implementation
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
		protected void TriggerCallbacks () { callback.Invoke(); }
	//ENDOF private methods
	}
}