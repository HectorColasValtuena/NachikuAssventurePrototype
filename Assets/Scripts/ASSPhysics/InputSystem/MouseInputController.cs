using UnityEngine;

using InputSettings = ASSPhysics.SettingSystem.InputSettings; //InputSettings

using IViewportController = ASSPhysics.CameraSystem.IViewportController;
using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

namespace ASSPhysics.InputSystem
{
	public class MouseInputController :
		ASSPhysics.ControllerSystem.IController,
		IInputController
	{
	//const definitions
		private const string mouseXAxisName = "Mouse X";
		private const string mouseYAxisName = "Mouse Y";
	//ENDOF const definitions

	//private properties
		private float screenSizeFactor { get { return ControllerCache.viewportController.size; }}
	//ENDOF private properties

	//IController implementation
		public bool isValid
		{
			get { return true; }
		}
	//ENDOF IController implementation

	//IInputController implementation
		//returns a vector3 representing the movement of the mouse during the last frame
		public Vector3 delta { get { return new Vector3 (UnityEngine.Input.GetAxis(mouseXAxisName), UnityEngine.Input.GetAxis(mouseYAxisName), 0f); }}
		public Vector3 screenSpaceDelta { get { 
			return ControllerCache.viewportController.ScreenSpaceToWorldSpace(delta, worldSpace: false);
		}}

		//scaled delta for configurable controls
		public Vector3 scaledDelta { get { return delta * InputSettings.mouseDeltaScale * screenSizeFactor; }}

		//gets zoom input
		//public float zoomDelta { get { return Input.mouseScrollDelta.y * InputSettings.mouseScrollDeltaScale; }}
		public float zoomDelta
		{
			get {
				return -1 * (Input.mouseScrollDelta.y * InputSettings.mouseScrollDeltaScale);
				/* commented how to get scroll input through keyboard keys
				+ ((Input.GetKey(KeyCode.R))
					? (+ 0.1f)
					: (Input.GetKey(KeyCode.F))
						? (- 0.1f)
						: 0);
				*/
			}
		}

		//gets button pressed
		public bool GetButtonDown (int buttonID) { return Input.GetMouseButtonDown(buttonID); }
	//IInputController implementation
	}
}