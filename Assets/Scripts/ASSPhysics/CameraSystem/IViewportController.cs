using Rect = UnityEngine.Rect;
using Vector3 = UnityEngine.Vector3;
using Camera = UnityEngine.Camera;

namespace ASSPhysics.CameraSystem
{
	public interface IViewportController
	{
		Rect baseViewport {get;}		//original size of the viewport
		Rect currentViewport {get;}		//current size of the viewport
		float viewportHeight {get;}	//current height value of the viewport

		//transforms a Vector3 representing a screen point into a Vector3 representing the 2d position
		//if worldSpace is false, the returned Vector3 originates in the camera's position
		Vector3 ScreenSpaceToWorldSpace (Vector3 mousePosition, Camera pivotCamera = null, bool worldSpace = true);
	}
}