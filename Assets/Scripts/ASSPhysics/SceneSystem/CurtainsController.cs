using UnityEngine;

using AnimationNames = ASSPhysics.Constants.AnimationNames;

namespace ASSPhysics.SceneSystem
{
	public class CurtainsController : MonoBehaviour
	{
	//serialized properties
		public GameObject spotlightContainer;

		public Transform rightSheetNode;
		public Transform leftSheetNode;
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
			get { return instance.rightSheetNode.position.x < instance.leftSheetNode.position.x; }
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