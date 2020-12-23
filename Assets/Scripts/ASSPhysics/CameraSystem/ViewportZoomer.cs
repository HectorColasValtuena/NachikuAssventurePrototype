using UnityEngine;

using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

namespace ASSPhysics.CameraSystem
{
	//[RequireComponent(typeof(IViewportController))]
	public class ViewportZoomer : MonoBehaviour
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
		private float desiredSize;

		private Vector2? currentPosition;

		private IViewportController viewport; //cached reference to the camera this controller handles
	//ENDOF private fields

	//private properties
		private float zoomDelta { get { return ControllerCache.inputController.zoomDelta; }}
		private Vector2 inputPosition { get { return ControllerCache.toolManager.activeTool.position; }}
	//ENDOF private properties

	//MonoBehaviour lifecycle
		public void Awake ()
		{
			viewport = GetComponent<IViewportController>();
		}

		public void Start ()
		{
			Debug.Log("start");
			if (maxSizeFromSceneValue) { maxSize = viewport.size; }
			currentSize = viewport.size;
			desiredSize = currentSize;
		}

		public void Update ()
		{
			ProcessInput();
			LerpCameraSize();
			ApplyViewportChanges();
		}
	//ENDOF MonoBehaviour lifecycle

	//private methods
		private void ProcessInput ()
		{
			if (zoomDelta != 0)
			{
				desiredSize += zoomDelta;
				currentPosition = inputPosition;
			}
			else
			{
				currentPosition = null;
			}
		}

		private void LerpCameraSize ()
		{
			desiredSize = Mathf.Clamp(desiredSize, minSize, maxSize);
			currentSize = Mathf.Lerp(currentSize, desiredSize, zoomLerpRate);
		}

		private void ApplyViewportChanges ()
		{
			viewport.ChangeViewport(
				position: currentPosition,
				size: currentSize
			);
		}
	//ENDOF private methods
	}	
}