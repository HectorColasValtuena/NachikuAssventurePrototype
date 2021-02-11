using Debug = UnityEngine.Debug;

using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

using ITool = ASSPhysics.HandSystem.Tools.ITool;

namespace ASSPhysics.DialogSystem.DialogChangers
{
	public class DialogChangerOnActionGrabAutomated : DialogChangerOnConditionHeldBase
	{
	//serialized fields and properties
		private ITool[] toolList
		{
			get	{ return ControllerCache.toolManager.tools;	}
		}
	//ENDOF serialized fields and properties

	//base class abstract method implementation
		protected override bool CheckHeldCondition ()
		{
			foreach (ITool tool in toolList)
			{
				if (tool.auto) { return true; }
			}
			return false;
		}
	//ENDOF base class abstract method implementation
	}
}