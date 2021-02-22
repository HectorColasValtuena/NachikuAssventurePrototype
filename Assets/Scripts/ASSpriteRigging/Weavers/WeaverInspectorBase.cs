using UnityEngine;

namespace ASSpriteRigging.Weavers
{
	public abstract class WeaverInspectorBase
	:
		ASSpriteRigging.BaseInspectors.ArmableInspectorBase,
		IWeaverInspector
	{
		public ConfigurableJoint defaultJoint;	//Sample joint configuration
	}
}