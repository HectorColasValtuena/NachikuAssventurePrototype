using ITool = ASSPhysics.HandSystem.Tools.ITool; //ITool
using EInputState = ASSPhysics.InputSystem.EInputState;

namespace ASSPhysics.HandSystem.Actions
{
	public interface IAction 
	{
		//returns true if this action is currently doing something, like maintaining a grab or repeating a slapping pattern
		//bool ongoing {get;}
		//wether action is to automatically repeat
		bool auto {get;}
		//initialize the action with a reference to the parent tool
		//will return true if action is valid and functional
		bool Initialize (ITool parentTool);
		//receive state of corresponding input medium
		void Input (EInputState state);
		//try to set in automatic state. Returns true on success
		bool Automate ();
		//update automatic action. To be called once per frame while action is automated. returns false if automation stops
		bool AutomationUpdate ();
		//stop automation
		void DeAutomate ();
		//clears and finishes the action
		void Clear ();
		//returns true if this action is valid for this hand (targets in range 'n such)
		bool IsValid ();
	}
}