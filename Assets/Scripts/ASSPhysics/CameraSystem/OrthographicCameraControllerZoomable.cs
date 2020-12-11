using Rect = UnityEngine.Rect;
using SerializeField = UnityEngine.SerializeField;
using Mathf = UnityEngine.Mathf;

using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

namespace ASSPhysics.CameraSystem
{
	public class OrthographicCameraControllerZoomable : OrthoCameraControllerBase
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
		public override Rect currentViewport { get; protected set; }
	//ENDOF inherited property override

	//private fields
		private float currentSize;
		private float targetSize;
	//ENDOF private fields

	//MonoBehaviour lifecycle
		public override void Start ()
		{
			base.Start();
			if (maxSizeFromSceneValue) { maxSize = cameraComponent.orthographicSize; }
			currentSize = cameraComponent.orthographicSize;
			targetSize = cameraComponent.orthographicSize;
		}

		public void Update ()
		{
			UpdateTargetSize();
			UpdateCameraSize();
		}
	//ENDOF MonoBehaviour lifecycle

	//private methods
		private void UpdateTargetSize ()
		{
			targetSize += ControllerCache.inputController.zoomDelta;
			targetSize = Mathf.Clamp(targetSize, minSize, maxSize);
		}

		private void UpdateCameraSize ()
		{
			currentSize = Mathf.Lerp(currentSize, targetSize, zoomLerpRate);
			ApplyCameraSize(currentSize);
		}

		private void ApplyCameraSize (float targetSize)
		{
			cameraComponent.orthographicSize = targetSize;
			currentViewport = cameraComponent.EMRectFromOrthographicCamera();
		}
	//ENDOF private methods
	}	
}