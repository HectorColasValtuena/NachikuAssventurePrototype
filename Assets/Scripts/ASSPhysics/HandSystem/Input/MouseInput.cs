using UnityEngine;

namespace ASSPhysics.HandSystem.Input
{
	public static class MouseInput
	{
		private const string mouseXAxisName = "Mouse X";
		private const string mouseYAxisName = "Mouse Y";

		//returns a vector3 representing the movement of the mouse during the last frame
		public static Vector3 delta { get { return new Vector3 (UnityEngine.Input.GetAxis(mouseXAxisName), UnityEngine.Input.GetAxis(mouseYAxisName), 0f); }}

		//transforms a Vector3 representing a screen point into a Vector3 representing the 2d position
		public static Vector3 ScreenSpaceToWorldSpace (Vector3 mousePosition) { ScreenSpaceToWorldSpace (mousePosition, Camera.main); }
		public static Vector3 ScreenSpaceToWorldSpace (Vector3 mousePosition, Camera pivotCamera)
		{

		}
	}
}