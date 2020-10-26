using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ASSPhysics.SceneSystem
{
	public class ASSceneManager : MonoBehaviour
	{
	//Constants and enum definitions
		//private const float sceneLoadMinimum = 0.9f;
		private static class SceneNumbers
		{
			public static int LAUNCHER = 0;	//unused, included for consistency
			public static int CURTAINS = 1;
			public static int MAINMENU = 2;
		}
	//ENDOF Constants and enum definitions


	//static properties and methods
		private static ASSceneManager instance;
		public static void InitializeCurtains ()
		{
			SceneManager.LoadScene(SceneNumbers.CURTAINS, LoadSceneMode.Additive);
		}

		public static void InitializeMenu ()
		{
			StaticChangeScene(SceneNumbers.MAINMENU, 3.0f);
		}

		public static void StaticChangeScene (int targetScene, float minimumWait = 0.0f)
		{
			instance.ChangeScene(targetScene, minimumWait);
		}
	//ENDOF static properties and methods

	//private fields and properties
		private bool busy = false;	//kept true while performing a scene change
	//ENDOF private fields and properties

	//MonoBehaviour lifecycle implementation
		public void Awake ()
		{
			instance = this;
		}
	//ENDOF MonoBehaviour lifecycle implementation

	//private methods
		private void ChangeScene (int targetScene, float minimumWait = 0.0f)
		{
			if (busy) { return; }
			StartCoroutine(ChangeSceneAsync(targetScene, minimumWait));
		}
		private IEnumerator ChangeSceneAsync (int targetScene, float minimumWait = 0.0f)
		{
			busy = true;
			CurtainsController.open = false;	//close the curtains
			//wait until curtains are closed
			while (
				!CurtainsController.isCompletelyClosed
				//&& loadingScene.progress < sceneLoadMinimum
				//&& !loadingScene.isDone
			) {
				//Debug.Log("Load progress: " + loadingScene.progress);
				yield return null;
			}
			//unload previous scene before deploying next
			AsyncOperation unloadingScene =	UnloadActiveScene();
			if (unloadingScene != null)
			{
				while (!unloadingScene.isDone) { yield return null; }
			}
			//start loading next scene
			AsyncOperation loadingScene = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);

			yield return new WaitForSeconds(minimumWait);
			while (!loadingScene.isDone) { yield return null; }

			//once next scene is ready set it as active
			SetActiveScene(targetScene);

			//finally open the curtains
			CurtainsController.open = true;
			busy = false;
		}

		private AsyncOperation UnloadActiveScene ()
		{
			if (SceneManager.GetActiveScene().buildIndex == SceneNumbers.CURTAINS)
			{
				Debug.LogWarning("Cannot unload curtain scene - ignoring request");
				return null;
			}
			return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
		}

		private void SetActiveScene (int targetScene)
		{
			SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(targetScene));
		}

	//ENDOF private methods
	}
}