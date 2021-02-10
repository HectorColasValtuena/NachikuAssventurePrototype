using UnityEngine;

using IEnumerator = System.Collections.IEnumerator;

namespace ASSPhysics.DialogSystem.DialogControllers
{
	public abstract class DialogControllerBase : MonoBehaviour, IDialogController
	{
	//private fields
		private bool closing = false;
	//ENDOF private fields

	//IDialogController definition and basic implementation
		public virtual void Enable ()
		{
			gameObject.SetActive(true);
		}

		public virtual void AnimatedDisable (DParameterlessDelegate finishingCallback)
		{
			if (closing) { return; }
			closing = true;
			PerformClosure(finishingCallback);
		}

		public virtual void ForceDisable ()
		{
			gameObject.SetActive(false);
		}
	//ENDOF IDialogController definition

	//protected abstract method declaration
		protected abstract void PerformClosure (DParameterlessDelegate finishingCallback);
	//ENDOF protected abstract method declaration

	//MonoBehaviour lifecycle implementation
		public virtual void OnEnable ()
		{
			closing = false;
		}
	//ENDOF MonoBehaviour lifecycle implementation


	}
}