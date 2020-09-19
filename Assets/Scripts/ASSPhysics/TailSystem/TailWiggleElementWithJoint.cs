using UnityEngine;

namespace ASSPhysics.TailSystem
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class TailWiggleElementWithJoint : TailWiggleElementBase
	{
		protected override void MatchRotation ()
		{
			//transform.rotation = Quaternion.Slerp(transform.rotation, baseRotation * Quaternion.Euler(0f, 0f, targetRotation), lerpRate);
		}
	}
}