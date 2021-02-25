using UnityEngine;

namespace ASSpriteRigging.Inspectors
{
	public abstract class WeaverInspectorBase
		: ArmableInspectorBase, IWeaverInspector
	{
		//Weave interconnection joint configuration
		[SerializeField]
		private ConfigurableJoint _defaultWeavingJoint = null; 
		public ConfigurableJoint defaultWeavingJoint { get { return _defaultWeavingJoint; }}
	}
}