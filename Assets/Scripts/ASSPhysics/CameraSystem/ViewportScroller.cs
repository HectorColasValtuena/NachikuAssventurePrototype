using UnityEngine;

using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

namespace ASSPhysics.CameraSystem
{
	public class ViewportScroller : MonoBehaviour
	{
	//serialized properties
		[SerializeField]
		private float scrollingRate = 1f;

		[SerializeField]
		private float borderScrollLimits = 0.05f;
	//ENDOF serialized properties

		private RectCameraControllerScrollable scrollable;

	//private properties
		private Rect cameraRect { get { return ControllerCache.viewportController.rect; }}

		private float noScrollRadius { get { return 0.5f - borderScrollLimits; }}
	//ENDOF private properties

	//MonoBehaviour lifecycle
		public void Awake ()
		{
			scrollable = GetComponent<RectCameraControllerScrollable>();
		}

		public void Update ()
		{
			Vector2 scrollingVector = GetScrollingVector();

			if (scrollingVector.magnitude > 0)
			{
				scrollable.Scroll(scrollingVector * scrollingRate);
			}
		}
	//ENDOF MonoBehaviour lifecycle

	//private methods
		//calculates desired scrolling direction and intensity
		public Vector2 GetScrollingVector ()
		{
			//ControllerCache.toolManager.activeTool
			if (ControllerCache.toolManager.activeTool.auto)
			{
				return Vector2.zero;
			}

			//normalized distance = distance / camera size
			Vector2 normalizedDistance =
				((Vector2) ControllerCache.toolManager.activeTool.position - cameraRect.center)
				/ cameraRect.size;

			//cut out the inner rectangle by moving towards 0 so centered hands don't move the camera 
			Vector2 marginDistance = new Vector2(
				x: Mathf.MoveTowards(current: normalizedDistance.x, target: 0, maxDelta: noScrollRadius),
				y: Mathf.MoveTowards(current: normalizedDistance.y, target: 0, maxDelta: noScrollRadius)
			);
			/*
			Vector2 marginDistance = new Vector2(
				x: normalizedDistance.x - (Mathf.Sign(normalizedDistance.x) * noScrollRadius),
				y: normalizedDistance.y - (Mathf.Sign(normalizedDistance.y) * noScrollRadius)
			);
			//*/

			Vector2 scrollingMagnitude = marginDistance / borderScrollLimits;

			return new Vector2(
				x: Mathf.Clamp(scrollingMagnitude.x, -1, 1),
				y: Mathf.Clamp(scrollingMagnitude.y, -1, 1)
			);
		}
	//ENDOF private methods
	}
}
