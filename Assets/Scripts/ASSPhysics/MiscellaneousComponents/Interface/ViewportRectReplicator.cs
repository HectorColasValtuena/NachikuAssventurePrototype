using UnityEngine;

using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

using static ASSPhysics.CameraSystem.RectTransformExtensions;

namespace ASSPhysics.MiscellaneousComponents
{
	public class ViewportRectReplicator : MonoBehaviour
	{
	//private fields
		private RectTransform rectTransform;
	//ENDOF private fields

	//MonoBehaviour lifecycle
		public void Awake ()
		{
			rectTransform = (RectTransform) transform;
		/////////////////////////////////////////////////////////////////////////////////////////////
		//[TO-DO] Maybe it's convenient initializing the transform replicating EVERY target property?
		/////////////////////////////////////////////////////////////////////////////////////////////
		}

		public void LateUpdate()
		{
			rectTransform.EMSetRect(ControllerCache.viewportController.rect);
		}
	//ENDOF MonoBehaviour lifecycle
	}
}
