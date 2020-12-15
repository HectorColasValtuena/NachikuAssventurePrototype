using MonoBehaviour = UnityEngine.MonoBehaviour;

using ControllerProvider = ASSPhysics.ControllerSystem.ControllerProvider;

using ITool = ASSPhysics.HandSystem.Tools.ITool;

namespace ASSPhysics.HandSystem.Managers
{
	public abstract class ToolManagerBase : MonoBehaviour, IToolManager
	{
	//IToolManager implementation
		public abstract ITool activeTool {get;}
	//ENDOF IToolManager implementation

	//MonoBehaviour lifecycle
		public virtual void Awake ()
		{
			ControllerProvider.RegisterController<IToolManager>(this);
		}
	}
}