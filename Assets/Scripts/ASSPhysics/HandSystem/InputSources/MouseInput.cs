using UnityEngine;

using InputSettings = ASSPhysics.SettingSystem.InputSettings; //InputSettings

namespace ASSPhysics.HandSystem.InputSources
{
	public static class MouseInput
	{
		private const string mouseXAxisName = "Mouse X";
		private const string mouseYAxisName = "Mouse Y";
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//[TO-DO] CLEANING TIME!
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		private static Vector3 cameraDepthCorrection = new Vector3 (0f, 0f, 10f);

		//returns a vector3 representing the movement of the mouse during the last frame
		public static Vector3 delta { get { return new Vector3 (UnityEngine.Input.GetAxis(mouseXAxisName), UnityEngine.Input.GetAxis(mouseYAxisName), 0f); }}
		public static Vector3 screenSpaceDelta { get { return ScreenSpaceToWorldSpace(delta); }}

		//scaled delta for configurable controls

		public static Vector3 scaledDelta { get { return delta * InputSettings.mouseDeltaScale; }}

		//transforms a Vector3 representing a screen point into a Vector3 representing the 2d position
		//if correctPosition is true, the returned Vector3 originates in the camera's position
		public static Vector3 ScreenSpaceToWorldSpace (Vector3 mousePosition, bool correctPosition = false) { return ScreenSpaceToWorldSpace (mousePosition, Camera.main, correctPosition); }
		public static Vector3 ScreenSpaceToWorldSpace (Vector3 mousePosition, Camera pivotCamera, bool correctPosition = false)
		{
			Debug.Log("Mouse position: " + mousePosition);

			//Vector3 screenHalfSize = new Vector3 (Screen.width/2, Screen.height/2, 0f);
			//Vector3 screenSize = new Vector3 (Screen.width, Screen.height, 0f);
			//first correct the mousePosition into a vector with 0,0 originating in the screen's middlepoint (right under the camera)
			//Vector3 position = mousePosition - screenHalfSize;
			//normalize position
			//Vector3 position = Vector3Divide (mousePosition, screenSize);

			Vector3 position = Vector3.Scale(mousePosition, new Vector3 (1/Screen.width, 1/Screen.height, 0f));
			//now multiply into screen size ratio
			position = Vector3.Scale(position, GetCameraSize(pivotCamera));
			//finally correct world position if necessary
			if (correctPosition)
			{
				position = position + pivotCamera.transform.position + cameraDepthCorrection - (GetCameraSize(pivotCamera)/2);
			}

			Debug.Log("World position: " + position);
			return position;
		}

		/*
		//divides 2 Vector3 component-wise
		private static Vector3 Vector3Divide (Vector3 leftHand, Vector3 rightHand)
		{
			return new Vector3 (
				leftHand.x/rightHand.x,
				leftHand.y/rightHand.y,
				(rightHand.z != 0) ? leftHand.z/rightHand.z : 0
			);
		}
		*/

		private static Vector3 GetCameraSize (Camera pivotCamera) 
		{
			return new Vector3 (pivotCamera.orthographicSize * pivotCamera.aspect * 2,	pivotCamera.orthographicSize * 2, 0f);
		}
	}
}