using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

namespace ASSPhysics.SceneSystem
{
	public class SceneController :
		ASSPhysics.ControllerSystem.MonoBehaviourControllerBase <ISceneController>,
		ISceneController
	{
	//Constants and enum definitions
		//private const float sceneLoadMinimum = 0.9f;
		private static class SceneNumbers
		{
			public static readonly int LAUNCHER = 0;	//unused, included for consistency
			public static readonly int CURTAINS = 1;
			public static readonly int MAINMENU = 2;
			public static readonly int QUITTER = 3;
		}
	//ENDOF Constants and enum definitions


	//static properties and methods
		//initialize method manually launchs the curtains layer scene through unityengine's SceneManager
		public static void Initialize ()
		{
			SceneManager.LoadScene(SceneNumbers.CURTAINS, LoadSceneMode.Additive);
		}
	//ENDOF static properties and methods

	//private fields and properties
		private bool busy = false;	//kept true while performing a scene change
	//ENDOF private fields and properties

	//MonoBehaviour lifecycle implementation
		//on first instantiation, load the menu under the curtain
		public void Start ()
		{
			ChangeScene(SceneNumbers.MAINMENU, 1.0f);
		}
	//ENDOF MonoBehaviour lifecycle implementation

	//ISceneController implementation
		//input is enabled if curtains are open
		public bool inputEnabled 
		{
			get
			{
				return !ControllerCache.curtainController.isCompletelyClosed;
			}
		}

		public void ChangeScene (int targetScene, float minimumWait = 0.0f)
		{
			if (busy) { return; }
			StartCoroutine(ChangeSceneAsync(targetScene, minimumWait));
		}
	//ENDOF ISceneController implementation

	//private methods
		private IEnumerator ChangeSceneAsync (int targetScene, float minimumWait = 0.0f)
		{
			busy = true;
			ControllerCache.curtainController.open = false;	//close the curtains

			//wait until curtains are closed
			while (!ControllerCache.curtainController.isCompletelyClosed)
			{ yield return null; }

			//unload previous scene before deploying next
			AsyncOperation unloadingScene =	UnloadActiveScene();
			unloadingScene.allowSceneActivation = true;
			if (unloadingScene != null)
			{
				while (!unloadingScene.isDone) { yield return null; }
				Resources.UnloadUnusedAssets();
			}

			//start loading next scene
			AsyncOperation loadingScene = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);

			yield return new WaitForSeconds(minimumWait);
			while (!loadingScene.isDone) { yield return null; }

			//once next scene is ready set it as active
			SetActiveScene(targetScene);

			//finally open the curtains and wait until they're done
			ControllerCache.curtainController.open = true;

			while (ControllerCache.curtainController.isCompletelyClosed)
			{ yield return null; }

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