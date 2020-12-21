using UnityEngine;

using Axis = UnityEngine.RectTransform.Axis;

namespace ASSPhysics.CameraSystem
{
	public static class RectTransformExtensions
	{
		//alters rectTransform's dimensions and position according to given rect
		public static void EMSetRect (this RectTransform rectTransform, Rect rect)
		{
			//set position
			rectTransform.position = rect.position;

			//set width
			rectTransform.SetSizeWithCurrentAnchors(
				axis: Axis.Horizontal,
				size: rect.width
			);

			//set height
			rectTransform.SetSizeWithCurrentAnchors(
				axis: Axis.Vertical,
				size: rect.height
			);
		}
	}
}