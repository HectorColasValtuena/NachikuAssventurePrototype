using IChainElement = ASSPhysics.ChainSystem.IChainElement;

namespace ASSPhysics.PulseSystem
{
	public interface IPulsePropagator : IChainElement
	{
		//execute a pulse and propagate it in the corresponding direction after proper delay
		void Propagate (IPulseData pulseData);
	}
}