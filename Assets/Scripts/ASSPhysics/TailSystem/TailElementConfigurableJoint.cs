using UnityEngine;

using IPulseData = ASSPhysics.PulseSystem.PulseData.IPulseData;

namespace ASSPhysics.TailSystem
{
	public class TailElementSingleConfigurableJoint : TailElementBase
	{
	//serialized fields and properties
		public float lerpRate = 0.05f;
	//ENDOF serialized fields and properties

	//private fields and properties
		private ConfigurableJoint joint;	//managed joint. can only handle one joint, so single thread tails for this class
		private Quaternion targetRotation;	//target rotation to reach
		private Quaternion jointRotation	//current joint target rotation. We'll slerp this into our target rotation
		{
			get { return joint.targetRotation; }
			set { joint.targetRotation = value; }
		}
	//ENDOF private fields and properties

	//MonoBehaviour lifecycle implementation
		public void Awake ()
		{
			joint = GetComponent<ConfigurableJoint>();
		}
	//ENDOF MonoBehaviour lifecycle implementation

	//TailElementBase abstract method implementation
		//attempts to match current rotation with target rotation
		protected override void MatchRotation ()
		{
			jointRotation = Quaternion.Slerp(jointRotation, targetRotation, lerpRate);
		}
	//ENDOF TailElementBase abstract method implementation

	//IPulsePropagator abstract method implementation
		//execute a pulse and propagate it in the corresponding direction after proper delay	
			//jointed element handles the pulse by setting its rotation 
		protected override void DoPulse (IPulseData pulseData)
		{
			targetRotation = baseRotation * PulseToRotation(pulseData);
		}
	//ENDOF IPulsePropagator abstract method implementation

	//private methods
		//transform a pulse into a quaternion rotation 
			//rotation around Z axis is proportional to pulse intensity
			//and clamped between positive and negative rotationMax
		private Quaternion PulseToRotation (IPulseData pulseData)
		{
			return Quaternion.Euler(
				0, 0, Mathf.Clamp(
					pulseData.computedValue * rotationSoftLimit,
					-rotationMax,
					rotationMax
				)
			);
		}
	//ENDOF private methods
	}
}