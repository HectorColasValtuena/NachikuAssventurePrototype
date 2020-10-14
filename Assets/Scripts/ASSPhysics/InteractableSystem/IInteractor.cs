using EInputState = ASSPhysics.InputSystem.EInputState;

namespace ASSPhysics.InteractableSystem
{
	//interactable interface element, like a button
	public interface IInteractor
	{
		//activate this interactable
		void Input (EInputState state);
	}
}