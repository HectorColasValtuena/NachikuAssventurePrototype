using ITool = ASSPhysics.HandSystem.Tools.ITool;

namespace ASSPhysics.HandSystem.Managers
{
	public interface IToolManager : ASSPhysics.ControllerSystem.IController
	{
		ITool activeTool {get;}
	}
}