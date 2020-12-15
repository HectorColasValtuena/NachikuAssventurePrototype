using IViewportController = ASSPhysics.CameraSystem.IViewportController;
using IInputController = ASSPhysics.InputSystem.IInputController;
using IToolManager = ASSPhysics.HandSystem.Managers.IToolManager;

namespace ASSPhysics.ControllerSystem
{
	public static class ControllerCache
	{
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
	//ENDOF viewport controller

	//input controller
		private static IInputController _inputController;
		public static IInputController inputController
		{
			get
			{
				if (_inputController == null)
					{ _inputController = ControllerProvider.GetController<IInputController>(); }
				return _inputController;
			}
		}
	//ENDOF input controller

	//toolManager
		private static IToolManager _toolManager;
		public static IToolManager toolManager
		{
			get
			{
				if (_toolManager == null)
					{ _toolManager = ControllerProvider.GetController<IToolManager>(); }
				return _toolManager;
			}
		}
	//ENDOF input controller	
	}
}