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
		//finds one interactable around this interactor's tool transform
		private IInteractable FindInteractable ()
		{
			Collider[] colliderList = ActionSettings.interactorCheckSettings.GetCollidersInRange(transform);
			return (colliderList.Length > 0)
				? FindPrioritaryInteractable(colliderList)
				: null;
		}

		//get the IInteractable component with the highest priority among the list
		private IInteractable FindPrioritaryInteractable (Component[] componentList)
		{
			Debug.Log("componentList length: " + componentList.Length);
			IInteractable prioritaryInteractable = null;
			foreach (Component component in componentList)
			{
				IInteractable currentInteractable = component.GetComponent<IInteractable>();

				//check only if an interactable was found in the transform
				if (currentInteractable != null)
				{
					//if there is not a candidate already or its priority is higher than target's
					if (prioritaryInteractable == null
						|| currentInteractable.priority > prioritaryInteractable.priority)
					{
						Debug.Log(prioritaryInteractable?.priority);
						prioritaryInteractable = currentInteractable;
					}
				}
			}
			return prioritaryInteractable;
		}
	//ENDOF private methods
	}
}