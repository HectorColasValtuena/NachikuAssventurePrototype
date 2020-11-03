namespace ASSPhysics.PulseSystem
{
	public abstract class PulseDataSignedIntensity : IPulseData
	{
	//IPulseData implementation
		//modifier for pulse propagation time delay
		public float propagationDelayModifier { get; private set; }
		//propagation direction. 1 towards children, -1 towards parent, 0 default
		public int propagationDirection { get; private set; }
	//ENDOF IPulseData implementation

	//public fields and properties
		//intensity for the effects of the pulse
		public float pulseIntensity { get; private set; }
		//direction of the effects of the pulse. 1 positive -1 negative 0 default/random
		public int pulseSign { get; private set; }
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
