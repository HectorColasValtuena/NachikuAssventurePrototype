using UnityEngine;

using IPulseData = ASSPhysics.PulseSystem.PulseData.IPulseData;

namespace ASSPhysics.TailSystem
{
	public class TailElementSingleJoint : TailElementBase
	{
	//serialized fields and properties
		[SerializeField]
		private float _rotationRate = 45f;
		public float rotationRate { get { return _rotationRate; } set { _rotationRate = value; } }

		[SerializeField]
		private float _lerpRate = 0.05f;
		public float lerpRate { get { return _lerpRate; } set { _lerpRate = value; } }

	//ENDOF serialized fields and properties

	//private fields and properties
		private ConfigurableJoint joint;	//managed joint. can only handle one joint, so single thread tails for this class
		private Quaternion desiredRotation;	//target rotation to reach
		private Quaternion expectedRotation;	//angle currently trying to achieve
		private Quaternion jointRotation	//current joint target rotation. We'll slerp this into our target rotation
		{
			get { return joint.targetRotation; }
			set { joint.targetRotation = value; }
		}
	//ENDOF private fields and properties

	//MonoBehaviour lifecycle implementation
		public override void Awake ()
		{
			base.Awake();
			joint = GetComponent<ConfigurableJoint>();
		}
	//ENDOF MonoBehaviour lifecycle implementation

	//TailElementBase abstract method implementation
		//attempts to match current rotation with target rotation
		protected override void MatchRotation ()
		{

			//jointRotation = Quaternion.Slerp(jointRotation, desiredRotation, lerpRate);
		}
	//ENDOF TailElementBase abstract method implementation

	//IPulsePropagator abstract method implementation
		//execute a pulse and propagate it in the corresponding direction after proper delay	
			//jointed element handles the pulse by setting its rotation 
		protected override void DoPulse (IPulseData pulseData)
		{
			Debug.Log("pulse");
			desiredRotation = PulseToQuaternion(pulseData); // * BaseRotation;
		}
	//ENDOF IPulsePropagator abstract method implementation

	//private methods
		//returns Z rotation required by a pulse
		private float PulseToAngle (IPulseData pulseData)
		{
			return Mathf.Clamp(
				pulseData.computedValue * rotationSoftLimit,
				-rotationMax,
				rotationMax
			);
		}

		//transform a pulse into a quaternion rotation 
			//rotation around Z axis is proportional to pulse intensity
			//and clamped between positive and negative rotationMax
		private Quaternion PulseToQuaternion (IPulseData pulseData)
		{
			return Quaternion.Euler(0, 0, PulseToAngle(pulseData));
		}
	//ENDOF private methods
	}
}