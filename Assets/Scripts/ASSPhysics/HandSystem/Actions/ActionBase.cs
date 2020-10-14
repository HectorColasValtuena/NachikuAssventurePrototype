using ASSPhysics.HandSystem.Tools; //ITool

/*DEBUG*/using UnityEngine;/*DEBUG*/

namespace ASSPhysics.HandSystem.Actions
{
	public abstract class ActionBase : IAction
	{
	//private fields and properties
		protected ITool tool;
	//ENDOF private fields and properties

	//IHandAction implementation
		//returns true if this action is currently doing something, like maintaining a grab or repeating a slapping pattern
		//base class only knows an action is ongoing if automatized. Children classes may determine additional conditions
		/*public virtual bool ongoing {
			get { return automatic; }
		}*/
		//wether action is to automatically repeat
		public bool auto { get { return _auto; } protected set { _auto = value; }}
		private bool _auto = false;

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
		public virtual bool Automate ()
		{
			return false;
		}

		//update automatic action. To be called once per frame while action is automated. returns false if automation stops
		public virtual bool AutomationUpdate ()
		{
			return false;
		}

		//stop automation
		public virtual void DeAutomate ()
		{
			//clear();
		}

		//clears and finishes the action
		public virtual void Clear ()
		{
			//auto = false; //unnecessary the tool is gonna be destroyed anyway
			tool.ActionEnded();
		}

		//returns true if this action is valid for this hand (targets in range 'n such)
		public virtual bool IsValid () { Debug.Log("ActionBase.IsValid()"); return false; }
	//ENDOF IHandAction implementation

	}
}