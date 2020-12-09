using Rect = UnityEngine.Rect;

namespace ASSPhysics.CameraSystem
{
	public class OrthographicCameraControllerZoomless : OrthgraphicCameraControllerBase
	{
		//abstract property implementation
			public override Rect baseViewport { get { return currentViewport; }}
		//ENDOF abstract property implementation
	}
}