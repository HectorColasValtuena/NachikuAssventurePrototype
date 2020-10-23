using UnityEngine.SceneManagement;

namespace ASSPhysics.SceneSystem
{
	public class ASSceneManager : MonoBehaviour
	{
	//Constants and enum definitions
		//private const float sceneLoadMinimum = 0.9f;
		private static enum ESceneNumbers : int
		{
			LAUNCHER = 0,	//unused, included for consistency
			CURTAINS = 1,
			MAINMENU = 2
		}
	//ENDOF Constants and enum definitions


	//static properties and methods
		private static ASSceneManager instance;
		public static void InitializeCurtain ()
		{
			SceneManager.LoadScene(SceneNumbers.CURTAINS, LoadSceneMode.Additive);
			StaticChangeScene(SceneNumbers.MAINMENU);
		}

		public static void StaticChangeScene (int targetScene)
		{
			instance.ChangeScene(targetScene);
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
		private void ChangeScene (int targetScene)
		{
			if (busy) { return; }
			StartCoroutine(ChangeSceneAsync(targetScene));
		}
		private IEnumerator ChangeSceneAsync (int targetScene)
		{
			busy = true;
			CurtainsController.open = false;	//close the curtains
			//start loading next scene
			AsyncOperation loadingScene = SceneManager.LoadSceneAsync(targetScene, LoadSceneMode.Additive);
			//wait until next scene is loaded and curtains are closed
			while (
				!CurtainsController.isCompletelyClosed
				//&& loadingScene.progress < sceneLoadMinimum
				&& loadingScene.isDone
			) {
				yield return null;
			}
			//once next scene is ready and curtains closed, unload previous scene and deploy next
			UnloadActiveScene();
			SetActiveScene(targetScene);
			//finally open the curtains
			CurtainsController.open = true;
			busy = false;
		}

		private void UnloadActiveScene ()
		{
			if (SceneManager.GetActiveScene().buildIndex == ESceneNumbers.CURTAINS)
			{
				Debug.LogWarning("Cannot unload curtain scene - ignoring request");
				return;
			}
			SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
		}

		private void SetActiveScene (int targetScene)
		{
			SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(targetScene));
		}

	//ENDOF private methods
	}
}