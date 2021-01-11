using IViewportController = ASSPhysics.CameraSystem.IViewportController;
using IInputController = ASSPhysics.InputSystem.IInputController;
using IToolManager = ASSPhysics.HandSystem.Managers.IToolManager;

namespace ASSPhysics.ControllerSystem
{
	public static class ControllerCache
	{
	//private methods
		//will return false if controller needs to be refreshed
		private static bool ControllerIsValid (IController controller)
		{
			return (controller != null && controller.isValid);
		}
	//ENDOF private methods

	//viewport controller
		private static IViewportController _viewportController;
		public static IViewportController viewportController
		{
			get	
			{
				if (!ControllerIsValid(_viewportController))
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
				if (!ControllerIsValid(_inputController))
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
				if (!ControllerIsValid(_toolManager))
					{ _toolManager = ControllerProvider.GetController<IToolManager>(); }
				return _toolManager;
			}
		}
	//ENDOF input controller	
	}
}