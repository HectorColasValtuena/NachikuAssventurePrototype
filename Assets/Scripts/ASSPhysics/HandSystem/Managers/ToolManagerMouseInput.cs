using UnityEngine;

using ITool = ASSPhysics.HandSystem.Tools.ITool;
using ToolBase = ASSPhysics.HandSystem.Tools.ToolBase;

using ASSPhysics.InputSystem; //MouseInput, EInputState

namespace ASSPhysics.HandSystem.Managers
{
	public class ToolManagerMouseInput : ToolManagerBase
	{
	//MonoBehaviour Lifecycle implementation
		public void Start ()
		{
			RallyTools();
		}

		public void Update ()
		{
			ToolCycleCheck();
			UpdateFocusedToolPosition();
			UpdateFocusedToolInput();
		}
	//ENDOF MonoBehaviour Lifecycle implementation

	//serialized fields
		[SerializeField]
		private ITool[] toolList;	//list of hands
	//ENDOF serialized fields

	//private fields and properties
		private int focusedToolIndex;		//highligted and active hand
	//ENDOF private fields and properties

	//IToolManager implementation
		public override ITool activeTool { get { return toolList[focusedToolIndex]; }}
	//ENDOF IToolManager implementation

	//private method implementation
		//finds all tools in the scene and save them to our list
		private void RallyTools ()
		{
			toolList = (ITool[]) Object.FindObjectsOfType<ToolBase>(); //Object.FindObjectsOfType<ITool>();
			SetFocused(0);
			Debug.Log ("found " + toolList.Length + " hands");
		}

		private void SetFocused (int target)
		{
			//if cycling out of the list clamp back into range
			if (target >= toolList.Length || target < 0)
			{	//multiply by its own sign to ensure always positive then get the rest of length
				target = ((target * (int) Mathf.Sign((float) target))) % toolList.Length;
			}

			focusedToolIndex = target;

			//send every know tool an update on its status - true if they're focused false otherwise
			for (int i = 0, iLimit = toolList.Length; i < iLimit; i++)
			{
				toolList[i].focused = (i == focusedToolIndex);
			}
		}

		//checks for input corresponding to hand swap, and performs the action if necessa
		private void ToolCycleCheck ()
		{
			//[TO-DO] MouseInput should answer wether or not to swap active hands, not be directly requested the button input
			if (MouseInput.GetButtonDown(1))
			{
				SetFocused(focusedToolIndex+1);
			}
		}

		private void UpdateFocusedToolPosition ()
		{
			toolList[focusedToolIndex].Move(MouseInput.scaledDelta);
		}

		//checks for input corresponding to main action, sends the correct state to the tool
		private void UpdateFocusedToolInput ()
		{
			bool button = Input.GetMouseButton(0);
			bool buttonFirstDown = Input.GetMouseButtonDown(0);
			if (button && !buttonFirstDown)
			{
				toolList[focusedToolIndex].MainInput(EInputState.Held);
			}
			else if (buttonFirstDown)
			{
				toolList[focusedToolIndex].MainInput(EInputState.Started);
			}
			else if (Input.GetMouseButtonUp(0))
			{
				toolList[focusedToolIndex].MainInput(EInputState.Ended);
			}
		}
	//ENDOF private method implementation
	}
}