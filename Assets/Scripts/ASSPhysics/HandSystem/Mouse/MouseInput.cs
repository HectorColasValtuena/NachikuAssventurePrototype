using UnityEngine;

namespace ASSPhysics.HandSystem.Mouse
{
	public static class MouseInput
	{
		private const string mouseXAxisName = "Mouse X";
		private const string mouseYAxisName = "Mouse Y";

		public static Vector2 MouseDelta { get { return new Vector2 (Input.GetAxis(mouseXAxisName), Input.GetAxis(mouseYAxisName)); }}
	}
}