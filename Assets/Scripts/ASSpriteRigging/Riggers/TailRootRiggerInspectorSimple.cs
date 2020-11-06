using UnityEngine;

using UnityEngine.U2D.Animation; //SpriteSkin

using IPulsePropagator = ASSPhysics.PulseSystem.PulsePropagators.IPulsePropagator;

using TailElementSingleJoint = ASSPhysics.TailSystem.TailElementSingleJoint;
//using ASSPhysics.TailSystem;	//TailRootWiggle

namespace ASSpriteRigging.Riggers
{
	[RequireComponent(typeof(SpriteSkin))]
	//[RequireComponent(typeof(TailRootWiggle))]
	public class TailRootRiggerInspectorSimple : SpriteSkinRiggerInspectorBase
	{
		public Rigidbody defaultRigidbody; //Sample rigidbody configuration
		public Collider defaultCollider;
		public Joint defaultChainJoint; //Sample spring configuration
	}
}