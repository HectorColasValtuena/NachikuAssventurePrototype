using UnityEngine;
using System.Collections.Generic;	//List<T>

using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

using ASSistant.Comparers; //ComparerSortCollidersByDistance

namespace ASSPhysics.SettingSystem.ActionSettingTypes
{
	[CreateAssetMenu(fileName = "Data", menuName = "Action settings/Collision Radius settings", order = 1)]
	public class ActionSettingCollisionRadius : ScriptableObject
	{
		[Tooltip("Layers to check collision against")]
		public LayerMask layerMask;

		[Tooltip("Collision check radius")]
		public float radius = 1.0f;	

		[Tooltip("If true scale collision radius with screen size")]
		public bool screenScaled = true;

		[Tooltip("Maximum number of items returned. closest first. If -1, all available results will be returned")]
		public int maximumCollisions = -1; 

		[Tooltip("Wether to include trigger colliders in the search")]
		public bool detectTriggers = true;

		//calculates collision radius
		private float efectiveRadius { get {
			//If not screenScaled or viewport controller unavailable, return unscaled size
			//if scaled and available controller apply scale
			return (ControllerCache.viewportController == null || !screenScaled)
				?	radius
				:	radius * ControllerCache.viewportController.size;
		}}

		//returns the result of the collision check defined in this collision radius around origin
		public Collider[] GetCollidersInRange (Transform originTransform)
		{ return GetCollidersInRange(originTransform.position); }
		public Collider[] GetCollidersInRange (Vector3 originPosition)
		{
			//fetch all the colliders in range
			List<Collider> colliderList = new List<Collider>(Physics.OverlapSphere(
				position: originPosition,
				radius: efectiveRadius,
				layerMask: layerMask,
				queryTriggerInteraction: detectTriggers
					? QueryTriggerInteraction.Collide	//if detectTriggers, collide with trigger colliders
					: QueryTriggerInteraction.Ignore	//if !detectTriggers, ignore trigger colliders
			));

			//sort detected colliders by distance 
			colliderList.Sort(new ComparerSortCollidersByDistance(originPosition) as IComparer<Collider>);

			//return a maximum of N colliders according to maximumCollisions
			if (maximumCollisions < 0 || maximumCollisions > colliderList.Count)
			{
				return colliderList.ToArray();
			}

			return colliderList.GetRange(index: 0, count: maximumCollisions).ToArray();
		}
	}
}