using UnityEngine;

namespace ASSPhysics.HandSystem.Actions.ActionSettings
{
	[CreateAssetMenu(fileName = "Data", menuName = "Action settings/Collision Radius settings", order = 1)]
	public class ActionSettingCollisionRadius : ScriptableObject
	{
		public LayerMask layerMask;
		public float radius = 1.0f;
	}
}