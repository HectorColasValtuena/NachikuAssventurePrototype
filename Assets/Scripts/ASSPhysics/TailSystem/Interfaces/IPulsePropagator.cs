namespace ASSPhysics.TailSystem
{
	public interface IPulsePropagator : IChainElement<IPulsePropagator>
	{
		void Pulse (
			float pulseIntensity = 1.0f,				//intensity for the effects of the pulse
			int pulseSign = 0,							//direction of the effects of the pulse. 1 positive -1 negative 0 default/random
			float propagationDelayModifier = 1.0f,		//modifier for pulse propagation time delay
			int propagationDirection = 0				//propagation direction. 1 towards children, -1 towards parent, 0 default
		);
	}
}