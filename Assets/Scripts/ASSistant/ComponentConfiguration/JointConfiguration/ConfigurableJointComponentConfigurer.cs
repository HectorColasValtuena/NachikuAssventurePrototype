
using UnityEngine;

namespace ASSistant.ComponentConfiguration.JointConfiguration
{
	public static class ConfigurableJointComponentConfigurer
	{
	//public static methods
		//applies right-hand properties to left-hand objects. returns reference to altered object
		public static ConfigurableJoint EMApplySettings (
			this ConfigurableJoint _this,
			ConfigurableJoint sample,
			bool copyConnectedBody = false
		) {
			Debug.Log("ConfigurableJoint EMApplySettings(" + _this + ", " + sample + ")");

			//ConfigurableJoint members
			_this.angularXDrive = sample.angularXDrive;
			_this.angularXLimitSpring = sample.angularXLimitSpring;
			_this.angularXMotion = sample.angularXMotion;
			_this.angularYLimit = sample.angularYLimit;
			_this.angularYMotion = sample.angularYMotion;
			_this.angularYZDrive = sample.angularYZDrive;
			_this.angularYZLimitSpring = sample.angularYZLimitSpring;
			_this.angularZLimit = sample.angularZLimit;
			_this.angularZMotion = sample.angularZMotion;
			_this.configuredInWorldSpace = sample.configuredInWorldSpace;
			_this.highAngularXLimit = sample.highAngularXLimit;
			_this.linearLimit = sample.linearLimit;
			_this.linearLimitSpring = sample.linearLimitSpring;
			_this.lowAngularXLimit = sample.lowAngularXLimit;
			_this.projectionAngle = sample.projectionAngle;
			_this.projectionDistance = sample.projectionDistance;
			_this.projectionMode = sample.projectionMode;
			_this.rotationDriveMode = sample.rotationDriveMode;
			_this.secondaryAxis = sample.secondaryAxis;
			_this.slerpDrive = sample.slerpDrive;
			_this.swapBodies = sample.swapBodies;
			_this.targetAngularVelocity = sample.targetAngularVelocity;
			_this.targetPosition = sample.targetPosition;
			_this.targetRotation = sample.targetRotation;
			_this.targetVelocity = sample.targetVelocity;
			_this.xDrive = sample.xDrive;
			_this.xMotion = sample.xMotion;
			_this.yDrive = sample.yDrive;
			_this.yMotion = sample.yMotion;
			_this.zDrive = sample.zDrive;
			_this.zMotion = sample.zMotion;

			//Joint members
			_this.anchor = sample.anchor;
			_this.autoConfigureConnectedAnchor = sample.autoConfigureConnectedAnchor;
			_this.axis = sample.axis;
			_this.breakForce = sample.breakForce;
			_this.breakTorque = sample.breakTorque;
			_this.connectedAnchor = sample.connectedAnchor;
			_this.connectedMassScale = sample.connectedMassScale;
			_this.enableCollision = sample.enableCollision;
			_this.enablePreprocessing = sample.enablePreprocessing;
			_this.massScale = sample.massScale;

			if (copyConnectedBody)
			{ _this.connectedBody = sample.connectedBody; }

			return _this;
		}
	//ENDOF public static methods
		/*listing of required members
	
		//ConfigurableJoint members

			angularXDrive
			angularXLimitSpring
			angularXMotion
			angularYLimit
			angularYMotion
			angularYZDrive
			angularYZLimitSpring
			angularZLimit
			angularZMotion
			configuredInWorldSpace
			highAngularXLimit
			linearLimit
			linearLimitSpring
			lowAngularXLimit
			projectionAngle
			projectionDistance
			projectionMode
			rotationDriveMode
			secondaryAxis
			slerpDrive
			swapBodies
			targetAngularVelocity
			targetPosition
			targetRotation
			targetVelocity
			xDrive
			xMotion
			yDrive
			yMotion
			zDrive
			zMotion

		//Joint members

			anchor
			autoConfigureConnectedAnchor
			axis
			breakForce
			breakTorque
			connectedAnchor
			connectedMassScale
			enableCollision
			enablePreprocessing
			massScale

			connectedBody
		*/
	}
}
