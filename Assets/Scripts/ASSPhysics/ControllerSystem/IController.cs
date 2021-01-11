namespace ASSPhysics.ControllerSystem
{
	public interface IController
	{
		//should return false when stale
		bool isValid {get;}
	}
}