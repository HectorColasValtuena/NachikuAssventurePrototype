using UnityEngine;

using RectMath = ASSistant.ASSMath.RectMath;

namespace ASSPhysics.CameraSystem
{
	public static class CameraExtensions
	{
		//generates a rect from target camera worldspace viewport
		public static Rect EMRectFromOrthographicCamera (this Camera camera)
		{
			float height = camera.orthographicSize * 2;
			float width = height * camera.aspect;
			return RectMath.RectFromCenterAndSize(
				position: camera.transform.position,
				width: width,
				height: height
			);
		}
	}
}