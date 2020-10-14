﻿using EInputState = ASSPhysics.InputSystem.EInputState;

namespace ASSPhysics.InteractableSystem
{
	//interactable interface element, like a button
	public interface IInteractable
	{
		//activate this interactable
		void Interact(EInputState state);
	}
}