using UnityEngine;

using ASSPhysics.ChainSystem;
using RandomSign = ASSistant.ASSRandom.RandomSign;

namespace ASSPhysics.PulseSystem
{
	public class ChainElementPulsePropagatorBase<TPulseData> : ChainElementBase, IPulsePropagator<TPulseData>
		where TPulseData : IPulseData
	{
	//IPulsePropagator implementation
		public void Pulse (TPulseData pulseData)
		{
			///////////////////////////////////////////////////////////////////////////////////////////////////////////////
		}
	//ENDOF IPulsePropagator implementation

	//private methods
	//ENDOF private methods
	}
}