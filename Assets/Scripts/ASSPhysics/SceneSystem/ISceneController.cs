namespace ASSPhysics.SceneSystem
{
	public interface ISceneController : ASSPhysics.ControllerSystem.IController
	{
		void ChangeScene (int targetScene, float minimumWait = 0.0f);
	}
}