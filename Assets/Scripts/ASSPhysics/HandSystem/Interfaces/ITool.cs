using UnityEngine;

namespace ASSPhysics.HandSystem
{
	public interface ITool
	{
		Vector3 position {get; set;}	//position of the hand
		bool focused {get; set;}		//wether the hand is on focus or not

		void MainInput (EInputState state); //called with either an Started, Held, or Ended state
	}
}