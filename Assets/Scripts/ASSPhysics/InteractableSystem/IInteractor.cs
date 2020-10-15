using EInputState = ASSPhysics.InputSystem.EInputState;

namespace ASSPhysics.InteractableSystem
{
	//interactable interface element, like a button
	public interface IInteractor
	{
		//process input. returns true if an interactable is in range
		bool Input (EInputState state);

		//Check if hovering over a valid interactable
		bool IsHovering ();
	}
}