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
			//process the pulse, then propagate
			Pulse(pulseData.computedValue);

			//then transmit the pulse in the desired direction
			if (pulseData.propagationDirection > 0)
			{
				DelayedPropagation(pulseData, chainParent);
			}
			else if (pulseData.propagationDirection < 0)
			{
				foreach (IChainElement chainChild in chainChildren)
				{
					DelayedPropagation(pulseData, chainChild);
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

			yield return new WaitForSeconds(
				GetPropagationDelay(propagationTarget) * pulseData.propagationDelayModifier
			);

			propagationTarget.Propagate(
				pulseData.GetUpdatedPulse(
					Vector3.Distance(transform.position, propagationTarget.transform.position)
				)
			);
		}
	//ENDOF private methods

	//overridable methods
		public abstract void Pulse (IPulseData pulseData);
		protected abstract float GetPropagationDelay (IPulsePropagator target);
	//ENDOF overridable methods
	}
}