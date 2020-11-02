namespace ASSPhysics.PulseSystem
{
	public interface IPulseData
	{
		float propagationDelayModifier {get;}	//modifier for pulse propagation time delay. default 1
		int propagationDirection {get;}		//propagation direction. 1 towards children, -1 towards parent, 0 default. default 0

		float GetValueAndUpdate();		//returns the computed value of this pulse and updates its values
	}
}
