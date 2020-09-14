using UnityEngine;

namespace ASSPhysics.HandSystem.Actions.ActionSettings
{
	[CreateAssetMenu(fileName = "Data", menuName = "Action settings/Grab settings", order = 1)]
	public class ActionSettingsGrab : ScriptableObject
	{
		public LayerMask grabbableMask;
		public float radius = 1.0f;
	}
}