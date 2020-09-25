using UnityEngine;

namespace ASSPhysics.TailSystem
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class TailWiggleElementFixedJoint : TailWiggleElementBase
	{
		//private const float lerpRate = 0.001f;
		private RelativeJoint2D joint;

		public /*override*/ void Awake ()
		{
			//base.Awake();
			joint = transform.gameObject.GetComponent<RelativeJoint2D>();
		}

		protected override void MatchRotation ()
		{
			//transform.rotation = Quaternion.Slerp(transform.rotation, baseRotation * Quaternion.Euler(0f, 0f, targetRotation), lerpRate);
			//joint.referenceAngle = Mathf.SmoothStep()
			joint.angularOffset = offsetRotation;
		}


	}
}