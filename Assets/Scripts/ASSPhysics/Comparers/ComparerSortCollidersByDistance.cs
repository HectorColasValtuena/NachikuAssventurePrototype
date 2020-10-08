using UnityEngine;
using Type = System.Type;
using System.Reflection;	//PropertyInfo
using System.Collections.Generic;	//IComparer<T>

namespace ASSPhysics.Comparers
{
	//IComparer used to sort a list of items by its distance from a Vector3 worldspace position
	public class ComparerSortCollidersByDistance : IComparer<Collider>
	{
		private const string offsetPropertyName = "center";
		private static readonly BindingFlags defaultBindingFlags =
			BindingFlags.Instance |
			BindingFlags.Public |
			BindingFlags.SetProperty |
			BindingFlags.GetProperty;

		private Vector3 originPosition;


		//Constructor: Takes position to use as center for upcoming comparison
		public ComparerSortCollidersByDistance (Vector3 __originPosition)
		{
			originPosition = __originPosition;
		}

		public int Compare (Collider colliderA, Collider colliderB)
		{
			if (colliderA == colliderB) return 0;

			Vector3 colliderAPos = GetColliderAbsolutePosition(colliderA);
			Vector3 colliderBPos = GetColliderAbsolutePosition(colliderB);

			//if A is closer to origin, Difference sign is negative
			float distanceDifference =
				  Vector3.Distance(originPosition, colliderAPos)
				- Vector3.Distance(originPosition, colliderBPos);

			//return comparison result
			return (distanceDifference == 0)
				? 0	//if both colliders are at the same distance return 0, they are equal
				: (int) Mathf.Sign(distanceDifference); //otherwise return 1 or -1 indicating closer collider
		}

		//extension method letting a sphere collider report its worldspace position
		//considers its offset property if it has one
		private Vector3 GetColliderAbsolutePosition (Collider collider)
		{

			Vector3 offsetPosition = (Vector3) collider
				.GetType()					//fetch received collider's type signature
				.GetProperty(offsetPropertyName, defaultBindingFlags)	//try to fetch offset value property
				?.GetValue(collider);		//extract the value of the property IF it exists, store null otherwise

			//return collider's transform position adding offset value if available
			return	collider.transform.position
				  +	((offsetPosition != null)
						? offsetPosition
						: Vector3.zero);
		}
	}
}