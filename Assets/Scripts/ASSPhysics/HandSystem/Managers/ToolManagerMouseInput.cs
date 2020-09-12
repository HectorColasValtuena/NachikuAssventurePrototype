using UnityEngine;

using ASSPhysics.HandSystem.Tools; //ITool
using ASSPhysics.HandSystem.Input; //MouseInput

namespace ASSPhysics.HandSystem.Managers
{
	public class ToolManagerMouseInput : ToolManagerBase
	{
		[SerializeField]
		private ITool[] toolList;	//list of hands

		private int mainTool;		//highligted and active hand

		public void Update ()
		{
			ToolCycleCheck();
			UpdateMainTool();
			MainActionCheck();
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
			toolList[mainTool].Move(MouseInput.screenSpaceDelta);
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