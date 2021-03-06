using UnityEngine;

namespace ASSPhysics.MiscellaneousComponents.Kickers
{
	public abstract class KickerOnConditionHeldOnUpdateBase : KickerOnConditionHeldBase
	{
	//MonoBehaviour Lifecycle
		public virtual void Update()
		{
			UpdateCondition(Time.deltaTime);
		}
	//ENDOF MonoBehaviour Lifecycle
	}
}