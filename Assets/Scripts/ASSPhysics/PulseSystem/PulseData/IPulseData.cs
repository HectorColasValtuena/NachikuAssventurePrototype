using ASSPhysics.PulseSystem;

namespace ASSPhysics.PulseSystem.PulseData
{
	public interface IPulseData
	{
		float propagationDelayModifier {get;}	//modifier for pulse propagation time delay. default 1
		EPulseDirection propagationDirection {get;}			//propagation direction. 1 towards children, -1 towards parent, 0 default. default 0
		float computedValue {get;}				//final pulse value

		IPulseData GetUpdatedPulse(float distance = 1.0f);	//gets an updated copy of the pulse, as changed over target distance
	}
}
