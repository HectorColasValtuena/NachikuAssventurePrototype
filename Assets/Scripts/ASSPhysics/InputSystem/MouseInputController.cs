using UnityEngine;

using InputSettings = ASSPhysics.SettingSystem.InputSettings; //InputSettings

using IViewportController = ASSPhysics.CameraSystem.IViewportController;
using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

namespace ASSPhysics.InputSystem
{
	public class MouseInputController : IInputController
	{
	//const definitions
		private const string mouseXAxisName = "Mouse X";
		private const string mouseYAxisName = "Mouse Y";
	//ENDOF const definitions

	//private properties
		private float screenSizeFactor { get { return ControllerCache.viewportController.viewportHeight; }}
	//ENDOF private properties

	//IInputController implementation
		//returns a vector3 representing the movement of the mouse during the last frame
		public Vector3 delta { get { return new Vector3 (UnityEngine.Input.GetAxis(mouseXAxisName), UnityEngine.Input.GetAxis(mouseYAxisName), 0f); }}
		public Vector3 screenSpaceDelta { get { 
			return ControllerCache.viewportController.ScreenSpaceToWorldSpace(delta, worldSpace: false);
		}}

		//scaled delta for configurable controls
		public Vector3 scaledDelta { get { return delta * InputSettings.mouseDeltaScale * screenSizeFactor; }}

		//gets button pressed
		public bool GetButtonDown (int buttonID) { return Input.GetMouseButtonDown(buttonID); }

		//gets zoom input
		public float GetZoomDelta () { return Input.mouseScrollDelta.y; }
	//IInputController implementation

	}
}