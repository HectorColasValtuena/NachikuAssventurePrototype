namespace ASSPhysics.SceneSystem
{
	public interface ICurtainController : ASSPhysics.ControllerSystem.IController
	{
		//opens and closes the curtains, or returns the currently DESIRED state
		bool open {get; set;}

		//returns true if curtain has actually reached a closed state
		bool isCompletelyClosed {get;}
	}
}