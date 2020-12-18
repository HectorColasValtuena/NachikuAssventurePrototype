using Rect = UnityEngine.Rect;
using Vector3 = UnityEngine.Vector3;
using Camera = UnityEngine.Camera;

namespace ASSPhysics.CameraSystem
{
	public interface IViewportController
	{
		Rect rect {get;}		//current size of the viewport
		float size {get; set;}		//current height value of the viewport
		Vector2 position {get; set;}	//world-space position of the camera

		//transforms a screen point into a world position
		//if worldSpace is false, the returned Vector3 ignores camera transform position
		Vector2 ScreenSpaceToWorldSpace (Vector2 mousePosition, bool worldSpace = true);
		Vector3 ScreenSpaceToWorldSpace (Vector3 mousePosition, bool worldSpace = true);

		//Prevents position from going outside of this camera's boundaries
		Vector2 ClampPositionToViewport (Vector2 position);
		Vector3 ClampPositionToViewport (Vector3 position);
	}
}