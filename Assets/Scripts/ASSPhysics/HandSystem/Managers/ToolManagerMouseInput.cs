using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using ControllerProvider = ASSPhysics.ControllerSystem.ControllerProvider;
using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

using ITool = ASSPhysics.HandSystem.Tools.ITool;

using EInputState = ASSPhysics.InputSystem.EInputState;
using IInputController = ASSPhysics.InputSystem.IInputController;

namespace ASSPhysics.HandSystem.Managers
{
	public class ToolManagerMouseInput : ToolManagerBase
	{

	//private fields
		[SerializeField]
		private ASSPhysics.HandSystem.Tools.ToolBase[] initialToolPrefabs = {};

		private List<ITool> toolList;	//list of hands
		private int focusedToolIndex;		//highligted and active hand
		private IInputController inputController;	//input controller
	//ENDOF private fields

	//private properties
		//input is considered enabled if there are tools and scene controller allows it
		private bool inputEnabled
		{
			get
			{
				return
					toolList != null &&
					toolList.Any() &&
					ControllerCache.sceneController.inputEnabled;
			}
		}
	//ENDOF private properties

	//IToolManager implementation
		public override ITool activeTool { get { return toolList[focusedToolIndex]; }}
	//ENDOF IToolManager implementation

	//MonoBehaviour Lifecycle implementation
		//create a mouse input controller for oneself on start, and register with the central controller
		public override void Awake ()
		{
			base.Awake();
			inputController = new ASSPhysics.InputSystem.MouseInputController();
			ControllerProvider.RegisterController<IInputController>(inputController);
		}

		public void Start ()
		{
			StartCoroutine(InitializeToolsAfterInputEnabled());
		}

		public void Update ()
		{
			if (inputEnabled)
			{
				ToolCycleCheck();
				UpdateFocusedToolPosition();
				UpdateFocusedToolInput();
			}
		}
	//ENDOF MonoBehaviour Lifecycle implementation

	//private method implementation
		//initializes predefined tools as soon as input is enabled by sceneController
		private IEnumerator InitializeToolsAfterInputEnabled()
		{
			toolList = new List<ITool>();

			//wait until sceneController allows input before initializing tool list
			while (!ControllerCache.sceneController.inputEnabled)
			{ yield return null; }

			//create initial list of tools in the scene
			foreach (ITool toolPrefab in initialToolPrefabs)
			{
				CreateTool(toolPrefab);
			}
			SetFocused(0);
		}

		private void CreateTool (ITool toolPrefab)
		{
			toolList.Add(InstantiateAsTool(toolPrefab));
		}

		//set tool under target index as focused
		private void SetFocused (int target)
		{
			if (!toolList.Any())
			{
				Debug.LogWarning("Trying to focus on an empty hand list");
				return;
			}

			//if cycling out of the list clamp back into range
			if (target >= toolList.Count || target < 0)
			{	//multiply by its own sign to ensure always positive then get the rest of length
				target = ((target * (int) Mathf.Sign((float) target))) % toolList.Count;
			}

			focusedToolIndex = target;

			//send every known tool an update on its status - true if they're focused false otherwise
			for (int i = 0, iLimit = toolList.Count; i < iLimit; i++)
			{
				toolList[i].focused = (i == focusedToolIndex);
			}
		}

		//checks for input corresponding to hand swap, and performs the action if necessa
		private void ToolCycleCheck ()
		{
			//[TO-DO] inputController should answer wether or not to swap active hands, not be directly requested the button input
			if (inputController.GetButtonDown(1))
			{
				SetFocused(focusedToolIndex+1);
			}
		}

		private void UpdateFocusedToolPosition ()
		{
			activeTool.Move(inputController.scaledDelta);
		}

		//checks for input corresponding to main action, sends the correct state to the tool
		private void UpdateFocusedToolInput ()
		{
			bool button = Input.GetMouseButton(0);
			bool buttonFirstDown = Input.GetMouseButtonDown(0);
			if (button && !buttonFirstDown)
			{
				activeTool.MainInput(EInputState.Held);
			}
			else if (buttonFirstDown)
			{
				activeTool.MainInput(EInputState.Started);
			}
			else if (Input.GetMouseButtonUp(0))
			{
				activeTool.MainInput(EInputState.Ended);
			}
		}
	//ENDOF private method implementation
	}
}