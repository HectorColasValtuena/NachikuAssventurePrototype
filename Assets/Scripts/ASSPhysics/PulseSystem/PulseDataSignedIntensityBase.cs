namespace ASSPhysics.PulseSystem
{
	public abstract class PulseDataSignedIntensityBase : IPulseData
	{
	//IPulseData implementation
		//modifier for pulse propagation time delay
		public float propagationDelayModifier { get; private set; }
		//propagation direction. 1 towards children, -1 towards parent, 0 default
		public int propagationDirection { get; private set; }
		
		//final pulse value
		//basic signed intensity pulse returns intensity appl
		public virtual float computedValue { get { return pulseIntensity; }}

		//gets an updated copy of the pulse, as changed over target distance
		public abstract IPulseData GetUpdatedPulse (float distance);
	//ENDOF IPulseData implementation

	//private fields and properties
		//intensity for the effects of the pulse
		protected float pulseIntensity; // { get; private set; }
	//ENDOF private fields and properties

	//constructor
		public PulseDataSignedIntensityBase (
			float __pulseIntensity = 1.0f,
			float __propagationDelayModifier = 1.0f,
			EPulseDirection __propagationDirection = EPulseDirection.children
		) {
			pulseIntensity = __pulseIntensity;
			propagationDelayModifier = __propagationDelayModifier;
			propagationDirection = __propagationDirection;
		}
	//ENDOF constructor
	}
}
