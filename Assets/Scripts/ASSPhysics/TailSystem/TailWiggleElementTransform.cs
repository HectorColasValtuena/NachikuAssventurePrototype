using UnityEngine;

namespace ASSPhysics.TailSystem
{
	//
	public class TailWiggleElementTransform : TailWiggleElementBase
	{
		private const float lerpRate = 0.001f;
		protected override void MatchRotation ()
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, baseRotation * Quaternion.Euler(0f, 0f, offsetRotation), lerpRate);
		}
	}
}