using Debug = UnityEngine.Debug;

using TToolManager = ASSPhysics.HandSystem.Managers.ToolManagerBase;
using IAction = ASSPhysics.HandSystem.Actions.IAction;
using TTool = ASSPhysics.HandSystem.Tools.ToolBase;

using TActionGrab = ASSPhysics.HandSystem.Actions.ActionGrab;

namespace ASSPhysics.DialogSystem.DialogChangers
{
	public class DialogChangerOnActionGrabAutomated : DialogChangerOnConditionHeldBase
	{
	//serialized fields and properties
		[UnityEngine.SerializeField]
		private TTool[] handList;
	//ENDOF serialized fields and properties

	//base class abstract method implementation
		protected override bool CheckHeldCondition ()
		{
			foreach (TTool hand in handList)
			{
				if (hand.auto) { return true; }
			}
			return false;
		}
	//ENDOF base class abstract method implementation
	}
}