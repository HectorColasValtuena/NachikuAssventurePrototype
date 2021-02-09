using UnityEngine;
using AnimationNames = ASSPhysics.Constants.AnimationNames;

using TDialogChanger = ASSPhysics.DialogSystem.DialogChangers.DialogChangerBase;

namespace ASSPhysics.MiscellaneousComponents
{
	public class MenuLaunchIntro : MonoBehaviour
	{
		public Animator musicAnimator;

		public void KickIntro ()
		{
			//musicAnimator.SetBool(AnimationNames.Curtains.musicPlayEnabled, true);
			GameObject.Find("IntroDialogEnabler").GetComponent<TDialogChanger>().ChangeDialog();
		}
	}
}