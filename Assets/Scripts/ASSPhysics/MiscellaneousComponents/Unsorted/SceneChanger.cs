using UnityEngine;

using ASSceneManager = ASSPhysics.SceneSystem.ASSceneManager;

public class TMPSceneChange : MonoBehaviour
{
	public void GoToScene (int targetScene)
	{
		ASSceneManager.StaticChangeScene(targetScene);
	}
}
