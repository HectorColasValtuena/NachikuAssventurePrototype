using ASSPhysics.HandSystem.Tools; //ITool

/*DEBUG*/using UnityEngine;/*DEBUG*/

namespace ASSPhysics.HandSystem.Actions
{
	public abstract class ActionBase : IAction
	{
	//IHandAction implementation
		//returns true if this action is currently doing something, like maintaining a grab or repeating a slapping pattern
		//base class only knows an action is ongoing if automatized. Children classes may determine additional conditions
		/*public virtual bool ongoing {
			get { return automatic; }
		}*/
		//wether action is to automatically repeat
		public bool automatic {get {return _automatic;}}
		protected bool _automatic = false;

		//initialize the action with a reference to the parent tool
		public virtual bool Initialize (ITool parentTool)
		{
			Debug.Log("Action initializing:" + this + " received: " + parentTool);
			tool = parentTool;
			return IsValid();
		}

		//receive state of corresponding input medium
		public abstract void Input (EInputState state);

		//try to set in automatic state. Returns true on success
		public virtual bool Automatize ()
		{
			
		}


		//clears and finishes the action
		public virtual void Clear ()
		{
			automatic = false;
			tool.ActionEnded();
		}

		//returns true if this action is valid for this hand (targets in range 'n such)
		public virtual bool IsValid () { Debug.Log("ActionBase.IsValid()"); return false; }
	//ENDOF IHandAction implementation

		protected ITool tool;
	}
}