namespace ASSPhysics.SceneSystem
{
	public interface ICurtainController : ASSPhysics.ControllerSystem.IController
	{
		//opens and closes the curtains, or returns the currently DESIRED state
		bool open {get; set;}

		//returns the state of the transition between 1 and 0, 0 meaning fully closed 1 meaning fully opened
		float openingProgress {get;}

		//returns true if curtain has actually reached a closed state
		bool isCompletelyClosed {get;}
	}
}