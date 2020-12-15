using UnityEngine;

using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

namespace ASSPhysics.CameraSystem
{
	public class OrthoCameraControllerScrollZoomable : OrthoCameraControllerScrollable
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

	//private fields
		private float targetSize;
	//ENDOF private fields

	//base class overrides
		public override float size
		{
			get { return base.size; }
			set { targetSize = Mathf.Clamp(value, minSize, maxSize); }
		}

	//ENDOF base class overrides

	//MonoBehaviour lifecycle
		public override void Start ()
		{
			base.Start();
			if (maxSizeFromSceneValue) { maxSize = size; }
			targetSize = size;
		}

		//update camera size before scroll processes
		public override void Update ()
		{
			UpdateTargetSize(
				zoomDelta: ControllerCache.inputController.zoomDelta,
				centerPosition: ControllerCache.toolManager.activeTool.position
			);
			LerpCameraSize();

			base.Update();
		}
	//ENDOF MonoBehaviour lifecycle

	//private methods
		private void UpdateTargetSize (float zoomDelta, Vector3 centerPosition)
		{
			if (zoomDelta != 0)
			{
				size += zoomDelta;
				size = Mathf.Clamp(targetSize, minSize, maxSize);
				position = centerPosition;
			}
		}

		private void LerpCameraSize ()
		{
			cameraSize = Mathf.Lerp(cameraSize, targetSize, zoomLerpRate);
		}
	//ENDOF private methods
	}
}