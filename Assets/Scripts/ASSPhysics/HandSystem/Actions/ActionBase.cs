using ASSPhysics.HandSystem.Tools; //ITool

namespace ASSPhysics.HandSystem.Actions
{
	public abstract class ActionBase : IAction
	{
	//IHandAction implementation
		//wether action is to automatically repeat
		public bool automatic {get; set;}

		//initialize the action with a reference to the parent tool
		public virtual bool Initialize (ITool parentTool)
		{
			tool = parentTool;
			return IsValid();
		}

		//receive state of corresponding input medium
		public abstract void Input (EInputState state);

		//clears and finishes the action
		public virtual void Clear ()
		{
			tool.ActionEnded();
		}

		//returns true if this action is valid for this hand (targets in range 'n such)
		public virtual bool IsValid () { return true; }
	//ENDOF IHandAction implementation

		protected ITool tool;
	}
}