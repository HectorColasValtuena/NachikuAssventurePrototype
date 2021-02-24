using UnityEngine;

namespace ASSpriteRigging.Inspectors
{
	[RequireComponent(typeof(UnityEngine.U2D.Animation.SpriteSkin))]
	public abstract class TailRiggerInspectorJointChain
	:
		SpriteSkinRiggerInspectorBase,
		IJointChainRiggerInspector
	{
		public Rigidbody[] rootAnchorList;	//list of root anchor targets

		public ConfigurableJoint defaultChainJoint; //Sample spring configuration
		public ConfigurableJoint defaultRootAnchorJoint; //Sample spring configuration
	}
}