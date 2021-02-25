using UnityEngine;

namespace ASSpriteRigging.Inspectors
{
	[RequireComponent(typeof(UnityEngine.U2D.Animation.SpriteSkin))]
	public abstract class TailRiggerInspectorJointChain
	:
		SpriteSkinRiggerInspectorBase,
		IJointChainRiggerInspector
	{
		//list of root anchor targets
		[SerializeField]
		private Rigidbody[] _rootAnchorList = {};
		public Rigidbody[] rootAnchorList { get { return _rootAnchorList; }}


		//Sample chain spring configuration
		[SerializeField]
		private ConfigurableJoint _defaultChainJoint = null;
		public ConfigurableJoint defaultChainJoint { get { return _defaultChainJoint; }}

		//Sample root anchoring spring configuration
		[SerializeField]
		private ConfigurableJoint _defaultRootAnchorJoint = null;
		public ConfigurableJoint defaultRootAnchorJoint { get { return _defaultRootAnchorJoint; }}
	}
}