using UnityEngine;

namespace ASSpriteRigging.Inspectors
{
	public interface IJointChainRiggerInspector : IRiggerInspector
	{
		//list of root anchor targets
		Rigidbody[] rootAnchorList {get;}

		//Sample chain spring configuration
		ConfigurableJoint defaultChainJoint {get;}

		//Sample root anchoring spring configuration
		ConfigurableJoint defaultRootAnchorJoint {get;}
	}
}