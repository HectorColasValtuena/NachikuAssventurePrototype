using MonoBehaviour = UnityEngine.MonoBehaviour;

using ITool = ASSPhysics.HandSystem.Tools.ITool;

namespace ASSPhysics.HandSystem.Managers
{
	public abstract class ToolManagerBase : MonoBehaviour, IToolManager
	{
	//IToolManager implementation
		public abstract ITool activeTool {get;}
	//ENDOF IToolManager implementation
	}
}