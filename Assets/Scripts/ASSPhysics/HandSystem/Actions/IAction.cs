using ASSPhysics.HandSystem.Tools; //ITool

namespace ASSPhysics.HandSystem.Actions
{
	public interface IAction 
	{
		//wether action is to automatically repeat
		bool automatic {get; set;}
		//initialize the action with a reference to the parent tool
		//will return true if action is valid and functional
		bool Initialize (ITool parentTool);
		//receive state of corresponding input medium
		void Input (EInputState state);
		//clears and finishes the action
		void Clear ();
	}
}