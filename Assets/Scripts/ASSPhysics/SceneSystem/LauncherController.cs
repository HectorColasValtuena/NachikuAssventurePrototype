using System.Collections;
using UnityEngine;

namespace ASSPhysics.SceneSystem
{
	public class LauncherController : MonoBehaviour
	{
		//public GameObject SplashContainer
		public void Launch ()
		{
			CursorLocker.LockAndHideSystemCursor();
			SceneController.Initialize();
		}
	}
}