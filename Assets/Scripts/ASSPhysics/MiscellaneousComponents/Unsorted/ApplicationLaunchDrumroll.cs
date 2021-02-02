using UnityEngine;

using AnimationNames = ASSPhysics.Constants.AnimationNames;
using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

namespace ASSPhysics.MiscellaneousComponents
{
	public class ApplicationLaunchDrumroll : MonoBehaviour
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
			if (
				!done &&
				(ControllerCache.curtainController != null && !ControllerCache.curtainController.isCompletelyClosed)
			) {
				done = true;
				animator.SetBool(AnimationNames.Curtains.drumrollFinalClash, true);
				GetComponent<MenuLaunchIntro>().KickIntro();
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