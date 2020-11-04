using IChainElement = ASSPhysics.ChainSystem.IChainElement;

using IPulseData = ASSPhysics.PulseSystem.PulseData.IPulseData;

using Transform = UnityEngine.Transform;

namespace ASSPhysics.PulseSystem.PulsePropagators
{
	public interface IPulsePropagator : IChainElement
	{
		Transform transform {get;}

		//execute a pulse and propagate it in the corresponding direction after proper delay
		void Propagate (IPulseData pulseData);
	}
}