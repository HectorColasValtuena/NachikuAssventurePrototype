using System.Collections;
using UnityEngine;

using ASSPhysics.ChainSystem;
using IPulseData = ASSPhysics.PulseSystem.PulseData.IPulseData;

using EPulseDirection = ASSPhysics.PulseSystem.EPulseDirection;

namespace ASSPhysics.PulseSystem.PulsePropagators
{
	public abstract class ChainElementPulsePropagatorBase : ChainElementBase, IPulsePropagator
	{
	//IPulsePropagator implementation
		//propagates the pulse towards the parent or children elements depending on pulse propagation direction
		public void Pulse (IPulseData pulseData)
		{
			Debug.Log("ChainElementPulsePropagatorBase.Pulse()");
			//process the pulse, then propagate
			DoPulse(pulseData);

			//then transmit the pulse in the desired direction
			if (pulseData.propagationDirection == EPulseDirection.towardsParent)
			{
				DelayedPropagation(pulseData, chainParent);
			}
			else if (pulseData.propagationDirection == EPulseDirection.towardsChildren)
			{
				//foreach (IChainElement chainChild in chainChildren)
				for (int i = 0, iLimit = childCount; i < iLimit; i++)
				{
					DelayedPropagation(pulseData, GetChild(i));
				}
			}
			else
			{
				Debug.LogWarning("ChainElementPulsePropagatorBase.Pulse(): propagation direction is 0 - can't propagate");
			}
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
			propagationTarget.Pulse(
				pulseData.GetUpdatedPulse(
					distance: Vector3.Distance(transform.position, propagationTarget.transform.position)
				)
			);
		}
	//ENDOF private methods

	//overridable methods
		//Override this method to execute the logic related to the pulse
			//dis method is da method fo' da voodoo
		protected abstract void DoPulse (IPulseData pulseData);

		//get delay in seconds before propagation to target effectuates
		protected abstract float GetPropagationDelay (IPulsePropagator target);
	//ENDOF overridable methods
	}
}