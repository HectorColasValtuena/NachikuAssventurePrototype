using UnityEngine;

namespace ASSPhysics.HandSystem.Actions.ActionSettings
{
	[CreateAssetMenu(fileName = "Data", menuName = "Action settings/Spring Joint settings", order = 1)]
	public class ActionSettingJoint : ScriptableObject
	{
		public ConfigurableJoint sampleJoint;
	}
}