using UnityEngine;

namespace ASSPhysics.CameraSystem
{
	public interface IViewportController
	{
		Rect rect {get;}		//current size of the viewport
		float size {get;}		//current height value of the viewport
		Vector2 position {get;}	//world-space position of the camera

		//moves and resizes camera viewport
		void ChangeViewport (Vector2? position = null, float? size = null);

		//transforms a screen point into a world position
		//if worldSpace is false, the returned Vector3 ignores camera transform position
		Vector2 ScreenSpaceToWorldSpace (Vector2 mousePosition, bool worldSpace = true);

		//Prevents position from going outside of this camera's boundaries
		Vector2 ClampPositionToViewport (Vector2 position);
		Vector3 ClampPositionToViewport (Vector3 position);
	}
}