using UnityEngine;

using AnimationNames = ASSPhysics.Constants.AnimationNames;

namespace ASSPhysics.CurtainSystem
{
	public class CurtainsController : MonoBehaviour
	{
	//static properties and methods
		private static CurtainsController instance;

		public static bool open
		{
			get { return instance.animator.GetBool(AnimationNames.Curtains.open); }
			set { instance.animator.SetBool(AnimationNames.Curtains.open, value); }
		}
	//ENDOF static properties and methods

	//private fields and properties
		private Animator animator;
	//ENDOF private fields and properties

	//MonoBehaviour lifecycle implementation
		//on creation register this instance
		public void Awake ()
		{
			animator = GetComponent<Animator>();
			instance = this;
		}
	//ENDOF MonoBehaviour lifecycle implementation
	}
}