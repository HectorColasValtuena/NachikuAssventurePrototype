using UnityEngine;

using UnityEngine.U2D.Animation; //SpriteSkin

using IPulsePropagator = ASSPhysics.PulseSystem.PulsePropagators.IPulsePropagator;

using TailElementSingleJoint = ASSPhysics.TailSystem.TailElementSingleJoint;
//using ASSPhysics.TailSystem;	//TailRootWiggle

namespace ASSpriteRigging.Riggers
{
	[RequireComponent(typeof(SpriteSkin))]
	//[RequireComponent(typeof(TailRootWiggle))]
	public class TailRootRiggerInspector : SpriteSkinRiggerInspectorBase
	{
		//public TailRootWiggle tailRoot { get { return gameObject.GetComponent<TailRootWiggle>();}}

		public TailElementSingleJoint defaultTailElement;	//default tail element controller

		public Rigidbody defaultRigidbody; //Sample rigidbody configuration
		public Collider defaultCollider;
		//public SpringJoint2D defaultAnchor;	//Sample anchor spring (parent-connected) configuration
		//public Joint defaultTailAnchor; //Sample spring configuration
		public Joint defaultChainJoint; //Sample spring configuration
	}
}