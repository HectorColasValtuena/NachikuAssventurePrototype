using UnityEngine;

namespace ASSPhysics.CameraSystem
{
	public abstract class OrthgraphicCameraControllerBase : MonoBehaviour, IOrthographicCameraViewport
	{
		//private fields and properties
			[SerializeField]
			new private Camera camera;
		//ENDOF private fields and properties

		//IOrthographicCameraViewport implementation
			public abstract Rect baseViewport { get; }
			public virtual Rect currentViewport { get; protected set; }
		//ENDOF IOrthographicCameraViewport implementation

		//MonoBehaviour lifecycle implementation
			public void Awake ()
			{
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// [TO-DO] add oneself to the controller finder				
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
				if (camera == null) { camera = GetComponent<Camera>(); }
				currentViewport = camera.EMRectFromOrthographicCamera();
			}
		//ENDOF MonoBehaviour lifecycle implementation

		//private class methods
			//updates the cached viewport for the active camera			
			protected void UpdateCurrentViewport ()
			{
				currentViewport = camera.EMRectFromOrthographicCamera();
			}		
		//ENDOF private class methdos
	}
}