using UnityEngine; //Resources

namespace ASSPhysics.HandSystem.Actions.ActionSettings
{
	public static class ActionSettings
	{
		private const string surfaceGrabSettingsPath = "ScriptableObjects/ActionSettings/SurfaceGrabSettings";
		private const string tailGrabSettingsPath = "ScriptableObjects/ActionSettings/tailGrabSettings";

		private static ActionSettingsGrab _surfaceGrabSettings;
		public static ActionSettingsGrab surfaceGrabSettings
		{
			get {
				return (_surfaceGrabSettings != null)? _surfaceGrabSettings : _surfaceGrabSettings = Resources.Load<ActionSettingsGrab>(surfaceGrabSettingsPath);
			}
		}

		private static ActionSettingsGrab _tailGrabSettings;
		public static ActionSettingsGrab tailGrabSettings
		{
			get {
				return (_tailGrabSettings != null)? _tailGrabSettings : _tailGrabSettings = Resources.Load<ActionSettingsGrab>(tailGrabSettingsPath);
			}
		}
	}
}