using System.Collections;
using UnityEngine;

using ASSPhysics.ChainSystem;

namespace ASSPhysics.PulseSystem
{
	public class ChainElementPulsePropagatorBase : ChainElementBase, IPulsePropagator
	{
	//IPulsePropagator implementation
		public void Pulse (IPulseData pulseData)
		{
			///////////////////////////////////////////////////////////////////////////////////////////////////////////////
		}
	//ENDOF IPulsePropagator implementation

	//private methods
		//propagates the pulse towards the parent or children elements depending on pulse propagation direction
		protected virtual void Propagate (IPulseData pulseData)
		{
			if (pulseData.propagationDirection > 0)
			{
				DelayedPropagation(pulseData, (chainParent as IPulsePropagator));
			}
			else if (pulseData.propagationDirection < 0)
			{
				foreach (IChainElement chainChild in chainChildren)
				{
					DelayedPropagation(pulseData, (chainChild as IPulsePropagator));
				}
			}
			Debug.LogWarning("ChainElementPulsePropagatorBase.Propagate(): propagation direction is 0 - can't propagate");
		}

		protected virtual IEnumerator DelayedPropagation (
			IPulseData pulseData,
			IPulsePropagator propagationTarget
		){

		}
	//ENDOF private methods
	}
}