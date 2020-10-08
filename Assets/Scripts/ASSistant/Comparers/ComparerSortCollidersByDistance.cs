using UnityEngine;
using System.Collections.Generic;	//IComparer<T>

using static ASSistant.ComponentConfiguration.ColliderPosition;

namespace ASSistant.Comparers
{
	//IComparer used to sort a list of items by its distance from a Vector3 worldspace position
	public class ComparerSortCollidersByDistance : IComparer<Collider>
	{
		private Vector3 originPosition;


		//Constructor: Takes position to use as center for upcoming comparison
		public ComparerSortCollidersByDistance (Vector3 __originPosition)
		{
			originPosition = __originPosition;
		}

		public int Compare (Collider colliderA, Collider colliderB)
		{
			if (colliderA == colliderB) return 0;

			Vector3 colliderAPos = colliderA.EMGetColliderAbsolutePosition();
			Vector3 colliderBPos = colliderB.EMGetColliderAbsolutePosition();

			//if A is closer to origin, Difference sign is negative
			float distanceDifference =
				  Vector3.Distance(originPosition, colliderAPos)
				- Vector3.Distance(originPosition, colliderBPos);

			//return comparison result
			return (distanceDifference == 0)
				? 0	//if both colliders are at the same distance return 0, they are equal
				: (int) Mathf.Sign(distanceDifference); //otherwise return 1 or -1 indicating closer collider
		}
	}
}