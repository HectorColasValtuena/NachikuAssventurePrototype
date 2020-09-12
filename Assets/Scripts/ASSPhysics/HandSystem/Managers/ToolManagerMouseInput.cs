using UnityEngine;

using ASSPhysics.HandSystem.Tools; //ITool
using ASSPhysics.HandSystem.InputSources; //MouseInput

namespace ASSPhysics.HandSystem.Managers
{
	public class ToolManagerMouseInput : ToolManagerBase
	{
	//MonoBehaviour Lifecycle implementation
		public void Awake ()
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

		[SerializeField]
		public ITool[] toolList;	//list of hands

		private int focusedTool;		//highligted and active hand

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

			focusedTool = target;

			//send every know tool an update on its status - true if they're focused false otherwise
			for (int i = 0, iLimit = toolList.Length; i < iLimit; i++)
			{
				toolList[i].focused = (i == focusedTool);
			}
		}

		//checks for input corresponding to hand swap, and performs the action if necessa
		private void ToolCycleCheck ()
		{
			if (Input.GetMouseButtonDown(1))
			{
				SetFocused(focusedTool+1);
			}
		}

		private void UpdateFocusedToolPosition ()
		{
			toolList[focusedTool].Move(MouseInput.scaledDelta);
		}

		//checks for input corresponding to main action, sends the correct state to the tool
		private void UpdateFocusedToolInput ()
		{
			bool button = Input.GetMouseButton(0);
			bool buttonFirstDown = Input.GetMouseButtonDown(0);
			EInputState state;
			if (button && !buttonFirstDown)
			{
				toolList[focusedTool].MainInput(EInputState.Held);
			}
			else if (buttonFirstDown)
			{
				toolList[focusedTool].MainInput(EInputState.Started);
			}
			else if (Input.GetMouseButtonUp(0))
			{
				toolList[focusedTool].MainInput(EInputState.Ended);
			}
		}
	}
}