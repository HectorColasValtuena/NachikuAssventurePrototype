using UnityEngine;

namespace ASSPhysics.HandSystem.Tools
{
	public interface ITool
	{
		Transform transform {get;}
		GameObject gameObject {get;}

		Vector3 position {get; set;}	//position of the hand
		bool focused {get; set;}		//wether the hand is on focus or not

		void Move (Vector3 delta);			//move the hand

		//called with either an Started, Held, or Ended state. also sets position if provided.
		void MainInput (EInputState state);
		void MainInput (EInputState state, Vector3 targetPosition);

		void ActionEnded (); //Called by the current action to remove itself
	}
}