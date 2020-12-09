using Rect = UnityEngine.Rect;

namespace ASSPhysics.CameraSystem
{
	public interface IOrthographicCameraViewport
	{
		Rect baseViewport {get;}
		Rect currentViewport {get;}
	}
}