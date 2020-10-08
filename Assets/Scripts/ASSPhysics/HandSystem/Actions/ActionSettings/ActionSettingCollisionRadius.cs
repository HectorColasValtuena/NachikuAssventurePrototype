using UnityEngine;
using System.Collections.Generic;	//List<T>

using ComparerColliderDistance = ASSPhysics.Comparers.ComparerSortCollidersByDistance;

namespace ASSPhysics.HandSystem.Actions.ActionSettings
{
	[CreateAssetMenu(fileName = "Data", menuName = "Action settings/Collision Radius settings", order = 1)]
	public class ActionSettingCollisionRadius : ScriptableObject
	{
		[Tooltip("Layers to check collision against")]
		public LayerMask layerMask;

		[Tooltip("Collision check radius")]
		public float radius = 1.0f;	

		[Tooltip("Maximum number of items returned. closest first. If -1, all available results will be returned")]
		public int maximumCollisions = -1; 

		[Tooltip("Wether to include trigger colliders in the search")]
		public bool detectTriggers = true;

		//returns the result of the collision check defined in this collision radius around origin
		public Collider[] GetCollidersInRange (Transform originTransform)
		{ return GetCollidersInRange(originTransform.position); }
		public Collider[] GetCollidersInRange (Vector3 originPosition)
		{
			//fetch all the colliders in range
			List<Collider> colliderList = new List<Collider>(Physics.OverlapSphere(
				position: originPosition,
				radius: radius,
				layerMask: layerMask,
				queryTriggerInteraction: detectTriggers
					? QueryTriggerInteraction.Collide	//if detectTriggers, collide with trigger colliders
					: QueryTriggerInteraction.Ignore	//if !detectTriggers, ignore trigger colliders
			));

			//sort detected colliders by distance 
			colliderList.Sort(new ComparerColliderDistance(originPosition) as IComparer<Collider>);

			//return a maximum of N colliders according to maximumCollisions
			if (maximumCollisions < -1 || maximumCollisions > colliderList.Count)
			{
				return colliderList.ToArray();
			}

			return colliderList.GetRange(index: 0, count: maximumCollisions).ToArray();
		}
	}
}