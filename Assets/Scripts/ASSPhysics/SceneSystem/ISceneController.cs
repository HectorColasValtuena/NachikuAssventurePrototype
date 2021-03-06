namespace ASSPhysics.SceneSystem
{
	public interface ISceneController : ASSPhysics.ControllerSystem.IController
	{
		//is input enabled or blocked
		bool inputEnabled { get; }

		//request a scene change using unity build index number
		void ChangeScene (int targetScene, float minimumWait = 0.0f);
	}
}