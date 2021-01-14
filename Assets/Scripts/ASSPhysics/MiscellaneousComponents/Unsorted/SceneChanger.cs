using UnityEngine;

using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

public class SceneChanger : MonoBehaviour
{
	public void GoToScene (int targetScene)
	{
		ControllerCache.sceneController.ChangeScene(targetScene);
	}
}
