using UnityEngine;

namespace ASSPhysics.TailSystem
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class TailElementConfigurableJoint : TailElementBase
	{
		private const float lerpRate = 0.1f;
		private ConfigurableJoint joint;

		//Calculates target rotation according to base and offset rotations
		private Quaternion absoluteTargetRotation
		{
			get { return baseRotation * Quaternion.Euler(0f, 0f, offsetRotation); }
		}

		public /*override*/ void Awake ()
		{
			//base.Awake();
			joint = transform.gameObject.GetComponent<ConfigurableJoint>();
		}


		//update joint target rotation towards target angular offset
		protected override void MatchRotation ()
		{

			//smooth target rotation towards target rotation
			joint.targetRotation = Quaternion.Slerp(
				joint.targetRotation, //transform.rotation,					
				absoluteTargetRotation,
				lerpRate
			);
		}

	}
}