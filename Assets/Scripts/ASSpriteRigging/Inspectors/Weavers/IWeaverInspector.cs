using ConfigurableJoint = UnityEngine.ConfigurableJoint;

namespace ASSpriteRigging.Inspectors
{
	public interface IWeaverInspector : IArmableInspector
	{
		ConfigurableJoint defaultWeavingJoint {get;}	//Sample joint configuration
	}
}