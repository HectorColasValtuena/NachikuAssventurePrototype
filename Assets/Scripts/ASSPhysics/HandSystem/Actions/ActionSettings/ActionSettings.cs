﻿using UnityEngine; //Resources

namespace ASSPhysics.HandSystem.Actions.ActionSettings
{
	//public definition of settings objects
	public static class ActionSettings
	{
		private const string surfaceGrabSettingsPath = "SurfaceGrabSettings";
		private static ActionSettingCollisionRadius _surfaceGrabSettings;
		public static ActionSettingCollisionRadius surfaceGrabSettings
		{ get {
			return (_surfaceGrabSettings != null)	//if cache is null, load from UnityEngine.Resources
				? _surfaceGrabSettings
				: _surfaceGrabSettings = Resources.Load<ActionSettingCollisionRadius>(surfaceGrabSettingsPath);
		}}

		private const string tailGrabSettingsPath = "TailGrabSettings";
		private static ActionSettingCollisionRadius _tailGrabSettings;
		public static ActionSettingCollisionRadius tailGrabSettings
		{ get {
			return (_tailGrabSettings != null)
				? _tailGrabSettings
				: _tailGrabSettings = Resources.Load<ActionSettingCollisionRadius>(tailGrabSettingsPath);
		}}

		private const string grabJointSettingsPath = "GrabJointSettings";
		private static ActionSettingSpringJoint _grabJointSettings;
		public static ActionSettingSpringJoint grabJointSettings
		{ get {
			return (_grabJointSettings != null)
				? _grabJointSettings
				: _grabJointSettings = Resources.Load<ActionSettingSpringJoint>(grabJointSettingsPath);
		}}
	}
}