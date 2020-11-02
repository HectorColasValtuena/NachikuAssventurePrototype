using IChainElement = ASSPhysics.ChainSystem.IChainElement;

namespace ASSPhysics.PulseSystem
{
	public interface IPulsePropagator<TPulseData> : IChainElement
		where TPulseData : IPulseData
	{
		//execute a pulse and propagate it in the corresponding direction after proper delay
		void Pulse (TPulseData pulseData);
		/*(
			float pulseIntensity = 1.0f,				//intensity for the effects of the pulse
			int pulseSign = 0,							//direction of the effects of the pulse. 1 positive -1 negative 0 default/random
			float propagationDelayModifier = 1.0f,		//modifier for pulse propagation time delay
			int propagationDirection = 0				//propagation direction. 1 towards children, -1 towards parent, 0 default
		);*/
	}
}