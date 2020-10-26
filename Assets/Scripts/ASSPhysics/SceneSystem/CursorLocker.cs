using Cursor = UnityEngine.Cursor;
using CursorLockMode = UnityEngine.CursorLockMode;

namespace ASSPhysics.SceneSystem
{
	public static class CursorLocker
	{
		public static void LockAndHideSystemCursor ()
		{
			Cursor.lockState = CursorLockMode.Locked; //Confined
			Cursor.visible = false;
		}
	}
}
