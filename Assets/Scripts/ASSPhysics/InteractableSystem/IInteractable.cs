using EInputState = ASSPhysics.InputSystem.EInputState;

namespace ASSPhysics.InteractableSystem
{
	//interactable interface element, like a button
	public interface IInteractable
	{
		//this interactable's Z sorting
		//Higher priority interactables will precede when touching muliple at once
		int priority { get; }

		//activate this interactable
		void Interact(EInputState state);
	}
}