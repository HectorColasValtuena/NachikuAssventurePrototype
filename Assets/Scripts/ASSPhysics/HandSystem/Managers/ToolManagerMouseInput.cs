﻿using UnityEngine;

using ASSPhysics.HandSystem.Tools; //ITool
using ASSPhysics.HandSystem.Input; //MouseInput

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
			UpdateMainTool();
			MainActionCheck();
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

		//checks for input corresponding to hand swap, and performs the action if necessary
		private void ToolCycleCheck ()
		{
			
		}

		//
		private void ToolCyclePerform ()
		{
			
		}

		private void UpdateMainTool ()
		{
			toolList[focusedTool].Move(MouseInput.scaledDelta);
			//toolList[focusedTool].Move(MouseInput.screenSpaceDelta);
			//BAD BAD BAD> toolList[focusedTool].position = MouseInput.ScreenSpaceToWorldSpace(UnityEngine.Input.mousePosition, true);
		}

		//checks for input corresponding to main action (grab/slap), and performs the action if necessary
		private void MainActionCheck ()
		{

		}

		private void MainActionPerform ()
		{

		}
	}
}