using UnityEngine;

using IEnumerator = System.Collections.IEnumerator;
using AnimationNames = ASSPhysics.Constants.AnimationNames;

namespace ASSPhysics.DialogSystem.DialogControllers
{
	public abstract class DialogControllerBase : MonoBehaviour, IDialogController
	{
	//serialized fields
		[SerializeField]
		protected Animator animator;
	//ENDOF serialized fields

	//private fields and properties
		protected DParameterlessDelegate queuedCallback = null;
		protected bool closing = false;
	//ENDOF private fields and properties

	//IDialogController implementation
		public virtual void Enable ()
		{
			gameObject.SetActive(true);
		}

		public virtual void AnimatedDisable (DParameterlessDelegate finishingCallback)
		{
			Debug.Log("activeDialog.AnimatedDisable");
			if (closing) { return; }
			closing = true;
			queuedCallback = finishingCallback;
			animator.SetTrigger(AnimationNames.Dialog.close);
		}

		public  virtual void ForceDisable ()
		{
			gameObject.SetActive(false);
		}
	//ENDOF IDialogController implementation

	//MonoBehaviour lifecycle implementation
		public void Awake ()
		{
			if (animator == null) { animator = GetComponent<Animator>(); }
		}

		public void OnEnable ()
		{
			closing = false;
		}
	//ENDOF MonoBehaviour lifecycle implementation

	//public methods
		public void ClosingAnimationFinishedCallback ()
		{
			Debug.Log("Closing animation finished");
			StartCoroutine(FinalizeClosing());
		}
	//ENDOF public methods

	//private methods
		private IEnumerator FinalizeClosing ()
		{
			//one-frame delay introduced to guarantee next dialog's animator has a chance to resize the panel before frame
			yield return new WaitForEndOfFrame();
			gameObject.SetActive(false);
			if (queuedCallback != null)
			{
				queuedCallback.Invoke();
			}
		}
	//ENDOF private methods
	}
}