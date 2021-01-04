using UnityEngine;

using RectMath = ASSistant.ASSMath.RectMath;
using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

namespace ASSPhysics.CameraSystem
{
	public class RectCameraControllerScrollable : RectCameraControllerSmooth
	{
	//public methods
		public void Scroll (Vector2 direction)
		{
			Vector2 movement = ScrollMovementFromDirection(direction);
			targetRect = RectMath.MoveRect(rect: targetRect, movement: movement);
			//ControllerCache.toolManager.activeTool.Move(movement);
		}
	//ENDOF public methods

	//private methods
		//scales direction vector by screen size, time delta, and rate modifier
		private Vector2 ScrollMovementFromDirection (Vector2 direction)
		{
			return direction * rect.height * Time.deltaTime;
		}
	//ENDOF private method
	}
}
