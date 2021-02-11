using Debug = UnityEngine.Debug;

using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

using ActionGrab = ASSPhysics.HandSystem.Actions.ActionGrab;

namespace ASSPhysics.DialogSystem.DialogChangers
{
	public class DialogChangerOnActionGrab : DialogChangerOnConditionHeldBase
	{
	//base class abstract method implementation
		protected override bool CheckHeldCondition ()
		{
			if (ControllerCache.toolManager == null)
			{
				Debug.LogWarning ("DialogChangerOnActionGrab: tool manager not found");
				return false;
			}

			//fetch active action cast as a grabbing action
			ActionGrab action = ControllerCache.toolManager.activeTool.activeAction as ActionGrab;

			//if action does not cast sucessfully return false
			if (action == null) { return false; }
			return action.grabActive;
		}
	//ENDOF base class abstract method implementation
	}
}