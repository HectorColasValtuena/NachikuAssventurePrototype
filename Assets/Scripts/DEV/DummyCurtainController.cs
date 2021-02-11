using UnityEngine;

using AnimationNames = ASSPhysics.Constants.AnimationNames;
using ICurtainController = ASSPhysics.SceneSystem.ICurtainController;

namespace DEV
{
	public class DummyCurtainController :
		ASSPhysics.ControllerSystem.MonoBehaviourControllerBase <ICurtainController>,
		ICurtainController
	{

	//ICurtainController implementation
		//opens and closes the curtains, or returns the currently DESIRED state
		public bool open
		{
			get { return true; }
			set {}
		}

		//returns true if curtain has actually reached a closed state
		public bool isCompletelyClosed { get { return false; } }
	//ENDOF ICurtainController implementation
	}
}