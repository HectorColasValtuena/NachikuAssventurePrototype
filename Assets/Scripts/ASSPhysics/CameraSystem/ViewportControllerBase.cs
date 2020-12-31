using UnityEngine;

using RectMath = ASSistant.ASSMath.RectMath;


namespace ASSPhysics.CameraSystem
{
	public abstract class ViewportControllerBase : MonoBehaviour, IViewportController
	{
	//abstract property declaration
		protected abstract Rect rect { get; }
	//ENDOF abstract property declaration

	//abstract method declaration
		protected abstract void ChangeViewport (Vector2? position, float? size);
	//ENDOF abstract method declaration

	//IViewportController implementation
		//dimensions and position of the viewport
		Rect IViewportController.rect
		{
			get { return publicRect; }
		}

		//current height value of the viewport
		float IViewportController.size
		{
			get { return publicRect.height; }
		}

		//current position
		Vector2 IViewportController.position
		{
			get { return publicRect.center; }
		}

		//moves and resizes camera viewport
		//if only one of the parameters is used the other aspect of the viewport is unchanged
		void IViewportController.ChangeViewport (
			Vector2? position,
			float? size
		) {
			ChangeViewport(position, size);
		}


		//transforms a screen point into a world position
		//if worldSpace is false, the returned Vector3 ignores camera transform position
		Vector2 IViewportController.ScreenSpaceToWorldSpace (
			Vector2 screenPosition,
			bool worldSpace
		) {
			//normalize position into a 0-1 range
			screenPosition = Vector2.Scale(screenPosition, new Vector2 (1/Screen.width, 1/Screen.height));

			//multiply normalized position by camera size
			Vector2 cameraSize = new Vector2 (publicRect.width, publicRect.height);
			screenPosition = Vector2.Scale(screenPosition, cameraSize);

			//finally correct world position if necessary
			if (worldSpace)
			{
				screenPosition = screenPosition + publicRect.center - (cameraSize/2);
			}

			return screenPosition;
		}

		//Prevents position from going outside of this camera's boundaries
		Vector2 IViewportController.ClampPositionToViewport (Vector2 position)
		{ return RectMath.ClampVector2WithinRect(position, publicRect); }
		Vector3 IViewportController.ClampPositionToViewport (Vector3 position)
		{ return RectMath.ClampVector3WithinRect(position, publicRect); }
	//ENDOF IViewportController implementation
	}
}