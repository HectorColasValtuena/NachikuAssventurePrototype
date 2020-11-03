using System.Collections;
using UnityEngine;

using ASSPhysics.ChainSystem;

namespace ASSPhysics.PulseSystem
{
	public abstract class ChainElementPulsePropagatorBase : ChainElementBase, IPulsePropagator
	{
	//IPulsePropagator implementation
		//propagates the pulse towards the parent or children elements depending on pulse propagation direction
		public void Propagate (IPulseData pulseData)
		{
			//first fetch pulse value, process the pulse, and update it
			Pulse(pulseData.GetValueAndUpdate());

			//then transmit the pulse in the desired direction
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

	//ENDOF IPulsePropagator implementation

	//private methods
		//propagate the pulse
		private IEnumerator DelayedPropagation (
			IPulseData pulseData,
			IPulsePropagator propagationTarget
		) {
			if (propagationTarget == null) return;

			yield return new WaitForSeconds(GetPropagationDelay(propagationTarget));

			propagationTarget.Propagate(pulseData);
		}
	//ENDOF private methods

	//overridable methods
		public abstract void Pulse (IPulseData pulseData);
		protected abstract float GetPropagationDelay (IPulsePropagator target);
	//ENDOF overridable methods
	}
}