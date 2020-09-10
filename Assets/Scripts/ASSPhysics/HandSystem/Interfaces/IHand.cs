using UnityEngine;

namespace ASSPhysics.HandSystem
{
	public interface IHand
	{
		Vector3 targetPosition {set;}

		void MainInput (EInputState state); //called with either an Started, Held, or Ended state
	}
}