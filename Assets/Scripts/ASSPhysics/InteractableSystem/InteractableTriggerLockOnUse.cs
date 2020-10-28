using UnityEngine;

using AnimationNames = ASSPhysics.Constants.AnimationNames; 
using EInputState = ASSPhysics.InputSystem.EInputState; //EInputState

namespace ASSPhysics.InteractableSystem
{
	//button that stays locked on after activation
	public class InteractableTriggerLockOnUse : InteractableTriggerOnRelease
	{
	//private fields and properties
		private bool lockedOn = false;
		protected override bool highlighted
		{
			get { return (lockedOn || base.highlighted); }
			set { base.highlighted = (lockedOn || value); }
		}
		protected override bool pressed
		{
			get { return (lockedOn || base.pressed); }
			set { base.pressed = (lockedOn || value); }
		}
	//ENDOF private fields and properties

	//overrides implementation
		protected override void TriggerCallbacks ()
		{
			if (lockedOn) return;
			lockedOn = true;
			base.TriggerCallbacks();
		}
	//overrides implementation
	}
}