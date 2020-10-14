using UnityEngine;
using EInputState = ASSPhysics.InputSystem.EInputState;

namespace ASSPhysics.InteractableSystem
{
	public class Interactor : MonoBehaviour, IInteractor
	{
	//IInteractor implementation
		public void Input (EInputState state) 
		{}
	//ENDOF IInteractor implementation

	//private methods
		private IInteractable FindInteractable () { return null; }
	//ENDOF private methods
	}
}