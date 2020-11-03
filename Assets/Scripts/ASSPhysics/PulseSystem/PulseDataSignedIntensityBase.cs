using Mathf = UnityEngine.Mathf;

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
		public virtual float computedValue {get { return pulseIntensity * Mathf.Sign(pulseSign); }}		

		//gets an updated copy of the pulse, as changed over target distance
		public abstract IPulseData GetUpdatedPulse (float distance);
	//ENDOF IPulseData implementation

	//private fields and properties
		//intensity for the effects of the pulse
		protected float pulseIntensity; // { get; private set; }
		//direction of the effects of the pulse. 1 positive -1 negative 0 default/random
		protected int pulseSign;// { get; private set; }
	//ENDOF private fields and properties

	//constructor
		public PulseDataSignedIntensityBase (
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
