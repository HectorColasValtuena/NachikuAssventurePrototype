//using UnityEngine;

namespace ASSPhysics.TailSystem
{
	public interface IPulsePropagator
	{
		public IPulsePropagator parent {get; set;}
		public IPulsePropagator[] children {get; set;}


		public void Pulse (
			float pulseIntensity = 1.0f,				//intensity for the effects of the pulse
			int pulseDirection = 0,						//direction of the effects of the pulse. 1 positive -1 negative 0 default/random
			float propagationDelayModifier = 1.0f,		//modifier for pulse propagation time delay
			int propagationDirection = 0				//propagation direction. 1 towards children, -1 towards parent, 0 default
		);
	}
}