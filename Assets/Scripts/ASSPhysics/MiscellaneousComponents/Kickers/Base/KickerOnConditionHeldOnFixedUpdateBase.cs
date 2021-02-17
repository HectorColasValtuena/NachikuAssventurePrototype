using UnityEngine;

namespace ASSPhysics.MiscellaneousComponents.Kickers
{
	public abstract class KickerOnConditionHeldOnFixedUpdateBase : KickerOnConditionHeldBase
	{
	//MonoBehaviour Lifecycle
		public void FixedUpdate()
		{
			UpdateCondition(Time.fixedDeltaTime);
		}
	//ENDOF MonoBehaviour Lifecycle
	}
}