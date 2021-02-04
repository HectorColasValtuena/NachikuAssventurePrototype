
using UnityEngine;

namespace ASSistant.ComponentConfiguration.JointConfiguration
{
	public static class ConfigurableJointSetChainAnchor
	{
	//public static methods
		//sets the anchors so they rest on the connected body's localspace 0,0,0
		public static ConfigurableJoint EMSetChainAnchor (
			this ConfigurableJoint _this
		) {
			_this.autoConfigureConnectedAnchor = false;
			_this.connectedAnchor = Vector3.zero;
			_this.anchor = _this.transform.InverseTransformPoint(_this.connectedBody.transform.position);

			return _this;
		}
	}
}
