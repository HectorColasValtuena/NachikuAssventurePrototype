using Mathf = UnityEngine.Mathf;

using ASSPhysics.PulseSystem;
using ASSistant.ASSRandom;

namespace ASSPhysics.PulseSystem.PulseData
{
	public class PulseDataSignedWaving : PulseDataSignedIntensityBase
	{
	//private fields and properties
		private float targetIntensity { get { return pulseMaximumIntensity * Mathf.Sign(pulseSign); } }
		private int pulseSign;	//direction of the effects of the pulse. 1 positive -1 negative 0 default/random
		private RandomRangeInt segmentLengthRange;	//random number of segments before sign change
		private float pulseMaximumIntensity;	//maximum intensity of the pulse
		private float pulseChangeSpeed;			//maximum pulse change per distance
	//ENDOF private fields and properties

	//constructor
		public PulseDataSignedWaving (
			RandomRangeInt __segmentLengthRange,
			float __pulseIntensity = 0.0f,
			float __propagationDelayModifier = 1.0f,
			EPulseDirection __propagationDirection = EPulseDirection.towardsChildren,
			int __pulseSign = 0,
			float __pulseMaximumIntensity = 1.0f,
			float __pulseChangeSpeed = 1.0f
		) : base (
			__pulseIntensity,
			__propagationDelayModifier,
			__propagationDirection
		) {
			segmentLengthRange = __segmentLengthRange;
			pulseSign = (__pulseSign != 0)
				? __pulseSign
				: GenerateSegmentSign(); //if a pulseSign is not provided generate a random sign
			pulseMaximumIntensity = __pulseMaximumIntensity;
			pulseChangeSpeed = __pulseChangeSpeed;
		}
	//ENDOF constructor

	//abstract method implementation
		//gets an updated copy of the pulse, as changed over target distance
			//waving pulse value fades between possitive and negative maximum value according to sign
			//sign changes every few segments randomly
		public override IPulseData GetUpdatedPulse (float distance)
		{
			int newSign = GetUpdatedSign();
			float newIntensity = GetUpdatedIntensity(newSign, distance);
			
			return (IPulseData) new PulseDataSignedWaving (
				__pulseIntensity: pulseIntensity,
				__propagationDelayModifier: propagationDelayModifier,
				__propagationDirection: propagationDirection,
				__pulseSign: newSign,
				__segmentLengthRange: segmentLengthRange,
				__pulseMaximumIntensity: pulseMaximumIntensity
			);

		}
	//ENDOF abstract method implementation

	//private methods
		//brign sign counter closer to zero
		private int GetUpdatedSign () { return GetUpdatedSign(pulseSign); }
		private int GetUpdatedSign (int sign)
		{
			//step sign towards zero
			int newSign = IntStepTowards(sign, 0);

			return (newSign != 0)		//if segment sign is depleted generate a new sign counter
				? newSign
				: GenerateSegmentSign();
		}

		//generates a segment sign counter using a random sign and segment length
		private int GenerateSegmentSign ()
		{
			return segmentLengthRange.Generate() * RandomSign.Generate();
		}

		////////////////////////////////////////////////////////////////////////////////
		//[TO-DO]: move dis elsewhere
		private int IntStepTowards (int value, int target)
		{
			return (value > target)	
							? value - 1	//if greater than target, decrement
							: (value < target)
							? value + 1 //if smaller than target, increment
							: target;	//if on target return on target
		}

		private float GetUpdatedIntensity (int targetSign, float distance)
		{
			return GetUpdatedIntensity(
				intensity: pulseIntensity,
				maximumIntensity: pulseMaximumIntensity,
				targetSign: targetSign,
				changeSpeed: pulseChangeSpeed,
				distance: distance
			);
		}
		private float GetUpdatedIntensity (
			float intensity,
			float maximumIntensity,
			int targetSign,
			float changeSpeed,
			float distance
		) {
			return Mathf.MoveTowards(
				current: intensity,
				target: maximumIntensity * Mathf.Sign(targetSign),
				maxDelta: changeSpeed * distance
			); 
		}
		//bring intensity closer to desired intensity
	//ENDOF private methods
	}
}
