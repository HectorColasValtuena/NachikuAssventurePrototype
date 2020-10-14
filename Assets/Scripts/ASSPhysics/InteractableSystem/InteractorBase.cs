using UnityEngine;
using EInputState = ASSPhysics.InputSystem.EInputState;

namespace ASSPhysics.InteractableSystem
{
	public class InteractorBase : MonoBehaviour, IInteractor
	{
		public void Input (EInputState state)
		{
		};
	}
}