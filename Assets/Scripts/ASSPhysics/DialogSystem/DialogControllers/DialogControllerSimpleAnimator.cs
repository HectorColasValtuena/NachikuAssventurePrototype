using UnityEngine;

using IEnumerator = System.Collections.IEnumerator;
using AnimationNames = ASSPhysics.Constants.AnimationNames;

namespace ASSPhysics.DialogSystem.DialogControllers
{
	public class DialogControllerSimpleAnimator : DialogControllerBase
	{
	//serialized fields
		[SerializeField]
		protected Animator animator;
	//ENDOF serialized fields

	//private fields and properties
	//ENDOF private fields and properties

	//inherited abstract method implementation
		protected override void PerformClosure ()
		{
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
			ForceDisable();
			InvokeFinishingCallback();
		}
	//ENDOF public methods

	//private methods	
	//ENDOF private methods
	}
}