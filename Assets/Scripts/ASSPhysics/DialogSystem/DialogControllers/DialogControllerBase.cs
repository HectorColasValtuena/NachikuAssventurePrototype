using UnityEngine;

using IEnumerator = System.Collections.IEnumerator;

namespace ASSPhysics.DialogSystem.DialogControllers
{
	public abstract class DialogControllerBase : MonoBehaviour, IDialogController
	{
	//private fields
		private bool closing = false;

		private DParameterlessDelegate queuedCallback = null;
	//ENDOF private fields

	//IDialogController definition and basic implementation
		//enable the dialog
		public virtual void Enable ()
		{
			gameObject.SetActive(true);
		}

		//disable the dialog. Stores finishingCallback for later execution
		public virtual void AnimatedDisable (DParameterlessDelegate finishingCallback)
		{
			if (closing) { return; }
			closing = true;
			queuedCallback = finishingCallback;
			PerformClosure();
		}

		//disable the dialog immediately
		public virtual void ForceDisable ()
		{
			gameObject.SetActive(false);
		}
	//ENDOF IDialogController definition

	//protected abstract method declaration
		protected abstract void PerformClosure ();
	//ENDOF protected abstract method declaration

	//protected method implementation
		protected void InvokeFinishingCallback ()
		{
			if (queuedCallback != null)
			{
				queuedCallback.Invoke();
				queuedCallback = null;
			}
		}
	//ENDOF protected method implementation

	//MonoBehaviour lifecycle implementation
		public virtual void OnEnable ()
		{
			closing = false;
		}
	//ENDOF MonoBehaviour lifecycle implementation


	}
}