using UnityEngine;

using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

namespace ASSPhysics.CameraSystem
{
	//[RequireComponent(typeof(IViewportController))]
	public class ViewportZoom : MonoBehaviour
	{
	//serialized fields
		[SerializeField]
		private float zoomLerpRate = 0.1f;
		[SerializeField]
		private float maxSize = 1f;
		[SerializeField]
		private float minSize = 0.25f;
		[SerializeField]
		private bool maxSizeFromSceneValue = true;
	//ENDOF serialized fields

	//inherited property override
	//ENDOF inherited property override

	//private fields
		private float currentSize;
		private float targetSize;
		
		protected IViewportController viewport; //cached reference to the camera this controller handles
	//ENDOF private fields

	//MonoBehaviour lifecycle
		public void Awake ()
		{
			viewport = GetComponent<IViewportController>();
		}

		public void Start ()
		{
			if (maxSizeFromSceneValue) { maxSize = viewport.size; }
			currentSize = viewport.size;
			targetSize = currentSize;
		}

		public void Update ()
		{
			UpdateTargetSize();
			LerpCameraSize();
		}
	//ENDOF MonoBehaviour lifecycle

	//private methods
		private void UpdateTargetSize ()
		{
			if (ControllerCache.inputController.zoomDelta != 0)
			{
				targetSize += ControllerCache.inputController.zoomDelta;
				targetSize = Mathf.Clamp(targetSize, minSize, maxSize);
				viewport.position = ControllerCache.toolManager.activeTool.position;
			}
		}

		private void LerpCameraSize ()
		{
			currentSize = Mathf.Lerp(currentSize, targetSize, zoomLerpRate);
			viewport.size = currentSize;
		}
	//ENDOF private methods
	}	
}