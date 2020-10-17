using Time = UnityEngine.Time;

using ASSistant.ASSRandom; //RandomRangeFloat

namespace ASSPhysics.MiscellaneousComponents.Kickers
{
	public abstract class KickerOnConditionBase : KickerBase
	{
	//serialized properties
		public RandomRangeFloat randomDelay;
	//ENDOF serialized properties 

	//private fields and properties
		private float currentDelay;
		private bool currentCheck;
	 	private bool previousCheck = false;
	//ENDOF private fields and properties

	//MonoBehaviour Lifecycle
	 	//on update check condition state change, timer update,
		public void FixedUpdate ()
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
				currentDelay -= Time.fixedDeltaTime;

				//if timer reaches zero, kick and reset timer
				if (currentDelay <= 0)
				{
					Kick();
					currentDelay = randomDelay.Generate();
				}
			}
			previousCheck = currentCheck;
		}

	//ENDOF MonoBehaviour Lifecycle

	//abstract method definition
		protected abstract bool CheckCondition ();
	//ENDOF abstract method definition
	}
}