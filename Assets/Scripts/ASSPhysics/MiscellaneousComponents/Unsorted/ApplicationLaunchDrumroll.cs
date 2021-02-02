using UnityEngine;

using AnimationNames = ASSPhysics.Constants.AnimationNames;
using ASSPhysics.SceneSystem;

namespace ASSPhysics.MiscellaneousComponents
{
	public class DrumrollController : MonoBehaviour
	{
	//serialized properties and fields
		public Animator animator;
	//ENDOF serialized properties and fields

	//private fields and properties
		private bool done = false;
	//ENDOF private fields and properties

	//Monobehaviour lifecycle
		public void Awake ()
		{
			if (animator == null) { animator = GetComponent<Animator>(); }
		}

		public void Update ()
		{
			if (!done && !CurtainsController.isCompletelyClosed)
			{
				animator.SetBool(AnimationNames.Curtains.drumrollFinalClash, true);
				GetComponent<IntroController>().KickIntro();
				done = true;
			}
		}
	//Monobehaviour lifecycle

	//Public methods
		//called by the animator when finished in order to destroy this controller for good as it's not needed anymore
		public void CleanUp ()
		{
			Destroy(gameObject);
		}
	//ENDOF Public methods
	}
}