using System.Collections;
using UnityEngine;

namespace ASSPhysics.SceneSystem
{
	public class LauncherController : MonoBehaviour
	{
		//public GameObject SplashContainer
		public void Launch ()
		{
			StartCoroutine(DelayedLaunch());
		}

		private IEnumerator DelayedLaunch ()
		{
			ASSceneManager.InitializeCurtains();
			yield return null;
			//yield return new WaitForSeconds(3f);
			ASSceneManager.InitializeMenu();
		}
	}
}