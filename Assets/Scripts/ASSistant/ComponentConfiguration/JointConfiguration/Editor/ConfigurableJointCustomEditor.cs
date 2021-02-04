using UnityEditor;
using UnityEngine;

namespace ASSistant.ComponentConfiguration.JointConfiguration
{
	[CustomEditor(typeof(ConfigurableJoint))]
	public class ConfigurableJointCustomEditor : Editor
	{
		public override void OnInspectorGUI ()
		{
			if (GUILayout.Button("Auto-configure anchors as chain"))
			{
				(target as ConfigurableJoint).EMSetChainAnchor();
			}
			base.OnInspectorGUI();
		}
	}
}
