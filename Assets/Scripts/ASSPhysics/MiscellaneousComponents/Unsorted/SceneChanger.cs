using UnityEngine;

using ASSceneManager = ASSPhysics.SceneSystem.ASSceneManager;

public class SceneChanger : MonoBehaviour
{
	public void GoToScene (int targetScene)
	{
		ASSceneManager.StaticChangeScene(targetScene);
	}
}
