using Rect = UnityEngine.Rect;
using Vector3 = UnityEngine.Vector3;
using Camera = UnityEngine.Camera;

namespace ASSPhysics.CameraSystem
{
	public interface IViewportController
	{
		Rect rect {get;}		//current size of the viewport
		float size {get; set;}		//current height value of the viewport
		Vector3 position {get; set;}	//world-space position of the camera

		//transforms a Vector3 representing a screen point into a Vector3 representing the 2d position
		//if worldSpace is false, the returned Vector3 ignores camera transform position
		Vector3 ScreenSpaceToWorldSpace (Vector3 mousePosition, bool worldSpace = true);

		//Prevents position from going outside of this camera's boundaries
		Vector3 ClampPositionToViewport (Vector3 position);
	}
}