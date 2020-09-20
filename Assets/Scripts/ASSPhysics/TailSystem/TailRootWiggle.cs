using System.Collections;
using System.Collections.Generic;
//using static System.Type; //Type

using UnityEngine;

namespace ASSPhysics.TailSystem
{
	public abstract class TailRootWiggle : MonoBehaviour
	{
		public Transform[] elementTransformList;
		private ITailElement[] elementControllerList;

		//public float speedBurstModifier = 1.0f;
		public float baseRandomInterval = 2.0f;
		public float maxTorsion = 45f;
		public float torsionAccelerationPerElement = 10f;
		public int directionSegmentSize = 24;

		//public float interElementLerp = 0.5f; //0 means every element will keep previous elements rotation. 1 means every element will use its own full rotation
		//public float maxRotationPerElement = 5f;

		public void Start ()
		{
			//on start cache element element list 
			elementControllerList = TransformListToComponentList(elementTransformList);
		}

		private ITailElement[] TransformListToComponentList (Transform[] transformList)
		{
			ITailElement[] componentList = new ITailElement[transformList.Length];
			for (int i = 0, iLimit = transformList.Length; i < iLimit; i++)
			{
				componentList[i] = transformList[i].gameObject.GetComponent<ITailElement>();
				if (componentList[i] == null) { Debug.LogError("Tail element missing controller: " + elementTransformList[i].name); }
			}
			return componentList;
		}

		public void Update ()
		{
			if (RandomTailMovementChance())
			{
				RandomizeTailProfile();
			}
		}


		private bool RandomTailMovementChance ()
		{
			return Random.value <= (Time.deltaTime / baseRandomInterval);
		}

		private void RandomizeTailProfile ()
		{
			//float previousRotation = 0f;
			float torsion = 0f;
			int direction = 0;
			for (int i = 0, iLimit = elementControllerList.Length; i < iLimit; i++)
			{
				//change directions every few segments randomly
				if (direction == 0)
				{
					direction = Random.Range(1, directionSegmentSize + 1) * ((Random.Range(0, 2) *2) -1);
					Debug.Log("New direction: " +  direction);
				}

				//randomly accelerate torsion in target direction
				torsion += Random.Range(0f, torsionAccelerationPerElement) * Time.deltaTime * Mathf.Sign((float)direction);
				torsion = Mathf.Clamp(torsion, -maxTorsion, maxTorsion);

				if (direction > 0) { direction--; } else { direction ++; }//move direction towards 0 so it gets randomly changed every several segments

				//rotate element by torsion degrees
				elementControllerList[i].targetRotation += torsion;
			}
		}
	}
}