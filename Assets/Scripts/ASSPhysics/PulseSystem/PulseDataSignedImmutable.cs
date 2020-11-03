namespace ASSPhysics.PulseSystem
{
	public class PulseDataSignedImmutable : PulseDataSignedIntensityBase
	{
	//abstract method implementation
		//gets an updated copy of the pulse, as changed over target distance
		//Base Signed Intensity pulse doesn't mutate
		public override IPulseData GetUpdatedPulse (float distance)
		{
			return this;
			/*
			return (IPulseData) new PulseDataSignedImmutable (
				__pulseIntensity: pulseIntensity,
				__pulseSign: pulseSign,
				__propagationDelayModifier: __propagationDelayModifier,
				__propagationDirection: propagationDirection
			);
			*/
		}
	//ENDOF abstract method implementation
	}
}
