using UnityEngine;

using PulseDataSignedWaving = ASSPhysics.PulseSystem.PulseData.PulseDataSignedWaving;
using EPulseDirection = ASSPhysics.PulseSystem.EPulseDirection;

using ASSistant.ASSRandom;

namespace ASSPhysics.TailSystem
{
	public class TailControllerPeriodicWaving : MonoBehaviour
	{
	//serialized fields and properties
		public TailElementBase firstTailElement;

		public float baseRandomInterval = 2.0f;

		public RandomRangeInt segmentLengthRange;
		public RandomRangeFloat pulseIntensityRange;

		public float pulseChangeSpeed = 1.0f;
		public float pulseMaximumIntensity = 1.0f;
		public float propagationDelayModifier = 1.0f;
	//ENDOF serialized fields and properties	

	//private fields and properties
	//ENDOF private fields and properties

	//MonoBehaviour lifecycle implementation
		public void Awake ()
		{
			if (firstTailElement == null) { firstTailElement = GetComponent<TailElementBase>(); }
		}

		public void Update ()
		{
			if (RandomTailMovementChance())
			{
				WaveTail();
			}
		}
	//ENDOF MonoBehaviour lifecycle implementation

	//private methods
		private bool RandomTailMovementChance ()
		{
			return Random.value <= (Time.deltaTime / baseRandomInterval);
		}

		//when initiating a waving movement, create a new pulse and start its propagation
		private void WaveTail ()
		{
			Debug.Log("Waving");
			firstTailElement.Pulse(new PulseDataSignedWaving(
				__segmentLengthRange: segmentLengthRange, //RandomRangeInt
				__pulseIntensity: pulseIntensityRange.Generate(), //float
				__propagationDelayModifier: propagationDelayModifier, //float
				__propagationDirection: EPulseDirection.towardsChildren, //EPulseDirection
				__pulseSign: 0, //int
				__pulseMaximumIntensity: pulseMaximumIntensity, //float
				__pulseChangeSpeed: pulseChangeSpeed//float
			));
		}
	//ENDOF private methods
	}
}