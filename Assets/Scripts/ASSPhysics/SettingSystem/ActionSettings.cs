using UnityEngine; //Resources

using ASSPhysics.SettingSystem.ActionSettingTypes;

namespace ASSPhysics.SettingSystem
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
		private static ActionSettingJoint _grabJointSettings;
		public static ActionSettingJoint grabJointSettings
		{ get {
			return (_grabJointSettings != null)
				? _grabJointSettings
				: _grabJointSettings = Resources.Load<ActionSettingJoint>(grabJointSettingsPath);
		}}

		private const string interactorAreaCheckSettingsPath = "InteractorAreaCheckSettings";
		private static ActionSettingCollisionRadius _interactorCheckSettings;
		public static ActionSettingCollisionRadius interactorCheckSettings
		{ get {
			return (_interactorCheckSettings != null)	//if cache is null, load from UnityEngine.Resources
				? _interactorCheckSettings
				: _interactorCheckSettings = Resources.Load<ActionSettingCollisionRadius>(interactorAreaCheckSettingsPath);
		}}

		private const string slapAreaSettingsPath = "SlapAreaSettings";
		private static ActionSettingCollisionRadius _slapAreaSettings;
		public static ActionSettingCollisionRadius slapAreaSettings
		{ get {
			return (_slapAreaSettings != null)	//if cache is null, load from UnityEngine.Resources
				? _slapAreaSettings
				: _slapAreaSettings = Resources.Load<ActionSettingCollisionRadius>(slapAreaSettingsPath);
		}}
	}
}