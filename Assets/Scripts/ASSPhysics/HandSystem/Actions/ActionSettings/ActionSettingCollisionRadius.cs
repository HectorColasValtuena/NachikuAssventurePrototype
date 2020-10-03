using UnityEngine;

namespace ASSPhysics.HandSystem.Actions.ActionSettings
{
	[CreateAssetMenu(fileName = "Data", menuName = "Action settings/Collision Radius settings", order = 1)]
	public class ActionSettingCollisionRadius : ScriptableObject
	{
		public LayerMask layerMask;	//Layers to check collision against
		public float radius = 1.0f;	//Collision check radius
		public int maximumCollisions = -1; //Maximum number of items returned. closest first. If -1, all available results will be returned

		//returns the result of the collision check defined in this collision radius around origin
		public Transform[] CheckCollision (Transform originTransform)
		{ return GetTransformsInRange(originTransform.position); }
		public Transform[] GetTransformsInRange (Vector3 originPosition)
		{

		}
	}
}