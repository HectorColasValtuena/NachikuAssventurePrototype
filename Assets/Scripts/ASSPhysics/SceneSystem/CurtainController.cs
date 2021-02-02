using UnityEngine;

using AnimationNames = ASSPhysics.Constants.AnimationNames;

namespace ASSPhysics.SceneSystem
{
	public class CurtainController : MonoBehaviour
	{
	//serialized properties
		public GameObject spotlightContainer;

		public Transform rightSheetUpperNode;
		public Transform leftSheetUpperNode;
		public Transform rightSheetLowerNode;
		public Transform leftSheetLowerNode;
	//ENDOF serialized properties

	//static properties and methods
		private static CurtainsController instance;
		public static bool open
		{
			get { return instance.curtainAnimator.GetBool(AnimationNames.Curtains.open); }
			set { instance.SetOpen(value); }
		}

		public static bool isCompletelyClosed
		{
			get
			{
				//return true if there is no curtain controller instance
				//Otherwise return true if both upper and lower nodes are beyond eachother
				return	instance == null ||
					(instance.rightSheetLowerNode.position.x < instance.leftSheetLowerNode.position.x &&
					instance.rightSheetUpperNode.position.x < instance.leftSheetUpperNode.position.x);
			}
		}
	//ENDOF static properties and methods

	//private fields and properties
		private Animator curtainAnimator;
		private Animator[] spotlightAnimators;
	//ENDOF private fields and properties

	//MonoBehaviour lifecycle implementation
		//on creation register this instance
		public void Awake ()
		{
			curtainAnimator = GetComponent<Animator>();
			spotlightAnimators = spotlightContainer.GetComponentsInChildren<Animator>();
			instance = this;
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