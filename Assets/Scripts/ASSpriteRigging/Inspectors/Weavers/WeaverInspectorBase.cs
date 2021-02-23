using UnityEngine;

namespace ASSpriteRigging.Inspectors
{
	public abstract class WeaverInspectorBase
		: ArmableInspectorBase, IWeaverInspector
	{
		public ConfigurableJoint defaultJoint;	//Sample joint configuration
	}
}