using System.Collections;
using UnityEngine;

using ASSPhysics.ChainSystem;
using IPulseData = ASSPhysics.PulseSystem.PulseData.IPulseData;

namespace ASSPhysics.PulseSystem.PulsePropagators
{
	public abstract class ChainElementPulsePropagatorBase : ChainElementBase, IPulsePropagator
	{
	//IPulsePropagator implementation
		//propagates the pulse towards the parent or children elements depending on pulse propagation direction
		public void Propagate (IPulseData pulseData)
		{
			//process the pulse, then propagate
			Pulse(pulseData);

			//then transmit the pulse in the desired direction
			if (pulseData.propagationDirection > 0)
			{
				DelayedPropagation(pulseData, chainParent);
			}
			else if (pulseData.propagationDirection < 0)
			{
				//foreach (IChainElement chainChild in chainChildren)
				for (int i = 0, iLimit = childCount; i < iLimit; i++)
				{
					DelayedPropagation(pulseData, GetChild(i));
				}
			}
			Debug.LogWarning("ChainElementPulsePropagatorBase.Propagate(): propagation direction is 0 - can't propagate");
		}
	//ENDOF IPulsePropagator implementation

	//private methods
		//propagate the pulse
		private void DelayedPropagation (IPulseData pulseData, IChainElement propagationTarget)
		{ DelayedPropagation(pulseData, (propagationTarget as IPulsePropagator)); }
		private void DelayedPropagation (IPulseData pulseData, IPulsePropagator propagationTarget)
		{ StartCoroutine(DelayedPropagationCoroutine(pulseData, propagationTarget)); }
		private IEnumerator DelayedPropagationCoroutine (IPulseData pulseData, IPulsePropagator propagationTarget)
		{
			if (propagationTarget == null) yield break;

			//wait for the propagation delay modified by the pulse's delay modifier
			yield return new WaitForSeconds(
				GetPropagationDelay(propagationTarget) * pulseData.propagationDelayModifier
			);

			//then propagate a copy of the pulse updated for the distance to the target
			propagationTarget.Propagate(
				pulseData.GetUpdatedPulse(
					distance: Vector3.Distance(transform.position, propagationTarget.transform.position)
				)
			);
		}
	//ENDOF private methods

	//overridable methods
		//Override this method to execute the logic related to the pulse
			//dis method is da method fo' da voodoo
		public abstract void Pulse (IPulseData pulseData);

		//get delay in seconds before propagation to target effectuates
		protected abstract float GetPropagationDelay (IPulsePropagator target);
	//ENDOF overridable methods
	}
}