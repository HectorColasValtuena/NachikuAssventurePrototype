namespace ASSPhysics.PulseSystem
{
	public interface IPulseData
	{
		float propagationDelayModifier;	//modifier for pulse propagation time delay. default 1
		int propagationDirection;		//propagation direction. 1 towards children, -1 towards parent, 0 default. default 0
	}
}
