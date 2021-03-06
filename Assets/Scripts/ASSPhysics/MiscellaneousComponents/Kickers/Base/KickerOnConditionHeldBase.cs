using UnityEngine;

using RandomRangeFloat = ASSistant.ASSRandom.RandomRangeFloat; //RandomRangeFloat

namespace ASSPhysics.MiscellaneousComponents.Kickers
{
	public abstract class KickerOnConditionHeldBase : MonoBehaviour, IKicker
	{
	//serialized properties
		[SerializeField]
		public RandomRangeFloat randomDelay = new RandomRangeFloat(1, 1);
	//ENDOF serialized properties 

	//private fields and properties
		private float currentDelay;
		private bool currentCheck;
	 	private bool previousCheck = false;
	//ENDOF private fields and properties

	//IKicker definition
		//executes a momentary effect
		public abstract void Kick ();
	//ENDOF IKicker definition

	//abstract method definition
		protected abstract bool CheckCondition ();
	//ENDOF abstract method definition

	//private methods
	 	//on update check condition state change, timer update
		protected void UpdateCondition (float timeDelta)
		{
			currentCheck = CheckCondition();

			//on condition state change to true, re-initialize timer
			if (currentCheck && !previousCheck)
			{
				currentDelay = randomDelay.Generate();
			}

			//if condition is true, decrement timer
			if (currentCheck)
			{
				currentDelay -= timeDelta;

				//if timer reaches zero, kick and reset timer
				if (currentDelay <= 0)
				{
					Kick();
					currentDelay = randomDelay.Generate();
				}
			}
			previousCheck = currentCheck;
		}
	//ENDOF private methods
	}
}