using UnityEngine;

namespace ASSPhysics.TailSystem
{
	//
	public class TailWiggleElementRigidbody : TailWiggleElementBase
	{
		protected override void MatchRotation ()
		{
			//transform.rotation = Quaternion.Slerp(transform.rotation, baseRotation * Quaternion.Euler(0f, 0f, targetRotation), lerpRate);
		}
	}
}