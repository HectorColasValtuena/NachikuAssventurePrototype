using IViewportController = ASSPhysics.CameraSystem.IViewportController;

namespace ASSPhysics.ControllerSystem
{
	public static class ControllerCache
	{
	//public properties
		//viewport controller
		private static IViewportController _viewportController;
		public static IViewportController viewportController
		{
			get	
			{
				if (_viewportController == null)
					{ _viewportController = ControllerProvider.GetController<IViewportController>(); }
				return _viewportController;
			}
		}
	//ENDOF public properties
	}
}