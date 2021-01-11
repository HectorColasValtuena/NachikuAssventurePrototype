using UnityEngine;

using ControllerProvider = ASSPhysics.ControllerSystem.ControllerProvider;

using ITool = ASSPhysics.HandSystem.Tools.ITool;

namespace ASSPhysics.HandSystem.Managers
{
	public abstract class ToolManagerBase :
		ASSPhysics.ControllerSystem.MonoBehaviourControllerBase<IToolManager>,
		IToolManager
	{
	//IToolManager implementation
		public abstract ITool activeTool {get;}
	//ENDOF IToolManager implementation
	}
}