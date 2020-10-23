using UnityEngine;

using AnimationNames = ASSPhysics.Constants.AnimationNames; 
using EInputState = ASSPhysics.InputSystem.EInputState; //EInputState

namespace ASSPhysics.InteractableSystem
{
	public class InteractableTriggerOnRelease : InteractableBase
	{
	//private fields and properties
		//wether this interactable is being pressed down by an active interactor
		//will trigger on input release if input started over this item
		private bool _pressed = false;
		protected bool pressed
		{
			get { return _pressed; }
			set
			{
				if (value != _pressed)
				{
					_pressed = value;
					if (animator != null)
						animator.SetBool(AnimationNames.Interactable.pressed, value);
				}
			}
		}
	//ENDOF private fields and properties

	//overrides implementation
	  //IInteractable implementation
		public override void Interact (EInputState state)
		{
			///////////////////////////////////////////////////////
			//[TO-DO]

			//if initiating a click over this button enter pressed state
			if (state == EInputState.Started)
			{
				pressed = true;
				//...
			}
			//if ending a click over this button execute
			else if (state == EInputState.Ended)
			{
				//...
				if (pressed) 
				{
					pressed = false;
					TriggerCallbacks();
				}
			}
		}

		//when highlight state changes to false, un-set pressed state
		protected override void HighlightChanged (bool state)
		{
			if (!state)	{ pressed = false; }
			base.HighlightChanged(state);
		}
	  //ENDOF IInteractable implementation
	//overrides implementation
	}
}