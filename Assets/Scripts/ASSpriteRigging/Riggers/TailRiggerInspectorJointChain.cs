using UnityEngine;

namespace ASSpriteRigging.Riggers
{
	[RequireComponent(typeof(UnityEngine.U2D.Animation.SpriteSkin))]
	public abstract class TailRiggerInspectorJointChain : SpriteSkinRiggerInspectorBase
	{
		public Rigidbody[] tailAnchorList;	//list of anchor targets
		public Rigidbody defaultRigidbody; //Sample rigidbody configuration
		public SphereCollider defaultCollider;
		public ConfigurableJoint defaultChainJoint; //Sample spring configuration
		public ConfigurableJoint defaultTailAnchorJoint; //Sample spring configuration
	}
}