using UnityEngine;

using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

namespace ASSPhysics.CameraSystem
{
	//[RequireComponent(typeof(IViewportController))]
	public class ViewportZoomer : MonoBehaviour
	{
	//serialized fields		
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
		private float _size;
		private float size
		{
			get { return _size; }
			set { _size = Mathf.Clamp(value: value, min: minSize, max: maxSize); }
		}

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
			size = viewport.size;
		}

		public void Update ()
		{
			ProcessInput();
		}
	//ENDOF MonoBehaviour lifecycle

	//private methods
		private void ProcessInput ()
		{
			if (zoomDelta != 0)
			{
				//[TO-DO] MAYBE it'd be good to scale zoomDelta by screen size
				size = size + zoomDelta;

				viewport.ChangeViewport(
					position: inputPosition,
					size: size
				);
			}
		}
	//ENDOF private methods
	}	
}