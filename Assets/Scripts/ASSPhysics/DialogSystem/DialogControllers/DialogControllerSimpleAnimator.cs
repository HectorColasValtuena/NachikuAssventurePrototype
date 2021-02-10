using UnityEngine;

namespace ASSPhysics.DialogSystem.DialogControllers
{
	public class DialogControllerSimpleAnimator : DialogControllerBase
	{
	//serialized fields
		[SerializeField]
		protected Animator animator;
	//ENDOF serialized fields

	//private fields and properties
		protected DParameterlessDelegate queuedCallback = null;
	//ENDOF private fields and properties

	//inherited abstract method implementation
		protected override void PerformClosure (DParameterlessDelegate finishingCallback)
		{
			queuedCallback = finishingCallback;
			animator.SetTrigger(AnimationNames.Dialog.close);
		}
	//ENDOF inherited abstract method implementation

	//MonoBehaviour lifecycle implementation
		public void Awake ()
		{
			if (animator == null) { animator = GetComponent<Animator>(); }
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