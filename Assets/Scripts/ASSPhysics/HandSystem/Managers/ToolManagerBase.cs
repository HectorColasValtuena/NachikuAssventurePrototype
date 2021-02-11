using UnityEngine;

using ControllerProvider = ASSPhysics.ControllerSystem.ControllerProvider;
using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

using ITool = ASSPhysics.HandSystem.Tools.ITool;

namespace ASSPhysics.HandSystem.Managers
{
	public abstract class ToolManagerBase :
		ASSPhysics.ControllerSystem.MonoBehaviourControllerBase<IToolManager>,
		IToolManager
	{
	//IToolManager implementation
		public abstract ITool[] tools {get;}
		public abstract ITool activeTool {get;}
	//ENDOF IToolManager implementation

	//protected class methods
		//instantiates a prefab of a tool
		protected ITool InstantiateAsTool (ITool prefabTool, Vector2? position = null)
		{
			//if no position is provided automatically pick the center of the screen
			if (position == null)
			{ position = ControllerCache.viewportController.position; }

			//create a copy of the tool and return a reference to its tool script
			return UnityEngine.Object.Instantiate(
				original: prefabTool.gameObject,
				position: (Vector2) position,
				rotation: prefabTool.transform.rotation,
				parent: transform.parent
			).GetComponent<ITool>();
		}
	//ENDOF protected class methods
	}
}