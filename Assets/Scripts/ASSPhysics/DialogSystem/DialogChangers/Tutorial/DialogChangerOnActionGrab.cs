using Debug = UnityEngine.Debug;

using TToolManager = ASSPhysics.HandSystem.Managers.ToolManagerBase;
using IAction = ASSPhysics.HandSystem.Actions.IAction;
using TTool = ASSPhysics.HandSystem.Tools.ToolBase;

using TActionGrab = ASSPhysics.HandSystem.Actions.ActionGrab;

namespace ASSPhysics.DialogSystem.DialogChangers
{
	public class DialogChangerOnActionGrab : DialogChangerOnConditionHeldBase
	{

	//private fields and properties
		[UnityEngine.SerializeField] private TToolManager _toolManager = null;
		private TToolManager toolManager
		{
			get
			{
				if (_toolManager == null)
				{ _toolManager = UnityEngine.Object.FindObjectOfType<TToolManager>();}
				return _toolManager;
			}
		}
	//ENDOF private fields and properties

	//base class abstract method implementation
		protected override bool CheckHeldCondition ()
		{
			if (toolManager == null)
			{
				Debug.LogWarning ("DialogChangerOnActionGrab: tool manager not found");
				return false;
			}

			//fetch current active tool
			//gather active action from it, cast as a grabbing action
			TActionGrab action = (
				(toolManager.activeTool as TTool)
				.activeAction as TActionGrab
			);
			//if action does not cast sucessfully return false
			if (action == null) { return false; }
			return action.grabActive;
		}
	//ENDOF base class abstract method implementation
	}
}