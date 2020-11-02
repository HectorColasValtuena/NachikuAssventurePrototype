namespace ASSPhysics.PulseSystem
{
	public class PulseDataSignedIntensity : IPulseData
	{
	//IPulseData implementation
		public float propagationDelayModifier;	//modifier for pulse propagation time delay
		public int propagationDirection;			//propagation direction. 1 towards children, -1 towards parent, 0 default
	//ENDOF IPulseData implementation

	//public fields and properties
		public float pulseIntensity;	//intensity for the effects of the pulse
		public int pulseSign;			//direction of the effects of the pulse. 1 positive -1 negative 0 default/random
	//ENDOF public fields and properties

	//constructor
		public PulseDataSignedIntensity (
			float __pulseIntensity = 1.0f,
			int __pulseSign = 0,
			float __propagationDelayModifier = 1.0f,
			int __propagationDirection = 0
		) {
			pulseIntensity = __pulseIntensity;
			pulseSign = __pulseSign;
			propagationDelayModifier = __propagationDelayModifier;
			propagationDirection = __propagationDirection;
		}
	//ENDOF constructor
	}
}
