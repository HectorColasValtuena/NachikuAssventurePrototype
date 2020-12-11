using Rect = UnityEngine.Rect;

namespace ASSPhysics.CameraSystem
{
	public class OrthographicCameraControllerZoomless : OrthoCameraControllerBase
	{
		//abstract property implementation
			public override Rect currentViewport { get { return baseViewport; } protected set {} }
		//ENDOF abstract property implementation
	}
}