using UnityEngine;
using EInputState = ASSPhysics.InputSystem.EInputState;
using ActionSettings = ASSPhysics.SettingSystem.ActionSettings; //interactorCheckSettings


namespace ASSPhysics.InteractableSystem
{
	public class Interactor : MonoBehaviour, IInteractor
	{
	//IInteractor implementation
		//process input. returns true if an interactable is in range
		public bool Input (EInputState state) 
		{
			IInteractable interactable = FindInteractable();
			interactable?.Interact(state);
			return (interactable != null);
		}

		//find if hovering over a valid interactable
		public bool IsHovering ()
		{
			return (FindInteractable() != null);
		}
	//ENDOF IInteractor implementation

	//private methods
		private IInteractable FindInteractable ()
		{
			Collider[] colliderList = ActionSettings.interactorCheckSettings.GetCollidersInRange(transform);
			return (colliderList.Length > 0)
				? colliderList[0].GetComponent<IInteractable>()
				: null;
		}
	//ENDOF private methods
	}
}