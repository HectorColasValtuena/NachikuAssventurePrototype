using UnityEngine;

namespace ASSPhysics.HandSystem.Input
{
	public static class MouseInput
	{
		private const string mouseXAxisName = "Mouse X";
		private const string mouseYAxisName = "Mouse Y";

		private static Vector3 cameraDepthCorrection = new Vector3 (0f, 0f, 10f);

		//returns a vector3 representing the movement of the mouse during the last frame
		public static Vector3 delta { get { return new Vector3 (UnityEngine.Input.GetAxis(mouseXAxisName), UnityEngine.Input.GetAxis(mouseYAxisName), 0f); }}
		public static Vector3 screenSpaceDelta { get { return ScreenSpaceToWorldSpace(delta); }}

		//transforms a Vector3 representing a screen point into a Vector3 representing the 2d position
		//if correctPosition is true, the returned Vector3 originates in the camera's position
		public static Vector3 ScreenSpaceToWorldSpace (Vector3 mousePosition, bool correctPosition = false) { return ScreenSpaceToWorldSpace (mousePosition, Camera.main); }
		public static Vector3 ScreenSpaceToWorldSpace (Vector3 mousePosition, Camera pivotCamera, bool correctPosition = false)
		{
			Vector3 screenHalfSize = new Vector3 (Screen.width/2, Screen.height/2, 0f);
			//first correct the mousePosition into a vector with 0,0 originating in the screen's middlepoint (right under the camera)
			Vector3 position = mousePosition - screenHalfSize;
			//normalize position
			position = Vector3Divide (position, screenHalfSize);
			//now multiply into screen size ratio
			position = Vector3.Scale(position, CameraOrthographicSize(pivotCamera));
			//finally correct world position if necessary
			if (correctPosition)
			{
				position = position + pivotCamera.transform.position + cameraDepthCorrection;
			}

			return position;
		}

		//divides 2 Vector3 component-wise
		private static Vector3 Vector3Divide (Vector3 leftHand, Vector3 rightHand)
		{
			return new Vector3 (
				leftHand.x/rightHand.x,
				leftHand.y/rightHand.y,
				leftHand.z/rightHand.z
			);
		}

		private static Vector3 CameraOrthographicSize (Camera pivotCamera) 
		{
			return new Vector3 (pivotCamera.orthographicSize * pivotCamera.aspect,	pivotCamera.orthographicSize, 0f);
		}
	}
}