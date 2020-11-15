using UnityEngine;

using EInputState = ASSPhysics.InputSystem.EInputState;
using IInteractor = ASSPhysics.InteractableSystem.IInteractor;

using IAction = ASSPhysics.HandSystem.Actions.IAction;

namespace ASSPhysics.HandSystem.Tools
{
	public interface ITool
	{
		Transform transform {get;}
		GameObject gameObject {get;}
		IInteractor interactor {get;}
		IAction activeAction {get;}

		Vector3 position {get; set;}	//position of the hand
		bool focused {get; set;}		//wether the hand is on focus or not

		//move the hand
		void Move (Vector3 delta);

		//called with either an Started, Held, or Ended state. also sets position if provided.
		void MainInput (EInputState state);
		void MainInput (EInputState state, Vector3 targetPosition);

		//Called by the current action to remove itself
		void ActionEnded ();

		//sets the animator
		void SetAnimationState (string triggerName);
	}
}