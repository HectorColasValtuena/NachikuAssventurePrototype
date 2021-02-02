using UnityEngine;

using AnimationNames = ASSPhysics.Constants.AnimationNames;

namespace ASSPhysics.SceneSystem
{
	public class CurtainController :
		ASSPhysics.ControllerSystem.MonoBehaviourControllerBase <ICurtainController>,
		ICurtainController
	{
	//private fields and properties
		//serialized fields
		[SerializeField]
		private GameObject spotlightContainer = null;

		[SerializeField]
		private Transform rightSheetUpperNode = null;
		[SerializeField]
		private Transform leftSheetUpperNode = null;
		[SerializeField]
		private Transform rightSheetLowerNode = null;
		[SerializeField]
		private Transform leftSheetLowerNode = null;
		//ENDOF serialized fields

		private Animator curtainAnimator;
		private Animator[] spotlightAnimators;
	//ENDOF private fields and properties

	//ICurtainController implementation
		//opens and closes the curtains, or returns the currently DESIRED state
		public bool open
		{
			get { return curtainAnimator.GetBool(AnimationNames.Curtains.open); }
			set { SetOpen(value); }
		}

		//returns true if curtain has actually reached a closed state
		public bool isCompletelyClosed
		{
			get
			{
				//true if both upper and lower nodes are beyond eachother
				return
					rightSheetLowerNode.position.x < leftSheetLowerNode.position.x &&
					rightSheetUpperNode.position.x < leftSheetUpperNode.position.x;
			}
		}
	//ENDOF ICurtainController implementation

	//MonoBehaviour lifecycle implementation
		//on creation register this instance
		public override void Awake ()
		{
			base.Awake();
			curtainAnimator = GetComponent<Animator>();
			spotlightAnimators = spotlightContainer.GetComponentsInChildren<Animator>();
		}
	//ENDOF MonoBehaviour lifecycle implementation

	//private methods
		private void SetOpen (bool value)
		{
			curtainAnimator.SetBool(AnimationNames.Curtains.open, value);
			foreach (Animator spotlightAnimator in spotlightAnimators)
			{
				spotlightAnimator.SetBool(AnimationNames.Curtains.spotlightFocused, value);
			}
		}
	//ENDOF private methods
	}
}