using UnityEngine;

namespace ASSPhysics.CameraSystem
{
	public static class CameraExtensions
	{
		//generates a rect from target camera worldspace viewport
		public static Rect EMRectFromOrthographicCamera (this Camera camera)
		{
			float height = camera.orthographicSize * 2;
			float width = height * camera.aspect;
			return new Rect(
				x: camera.transform.position.x - (width / 2),
				y: camera.transform.position.y - (height / 2),
				width: width,
				height: height
			);
		}
	}
}