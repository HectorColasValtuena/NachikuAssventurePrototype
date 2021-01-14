﻿using IViewportController = ASSPhysics.CameraSystem.IViewportController;
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

		//if controller is not up to date return a fresh reference
		private static TController ValidateController <TController> (TController controller)
			where TController : IController
		{
			if (ControllerIsValid(controller))
			{ return controller; }
			return ControllerProvider.GetController<TController>();
		}
	//ENDOF private methods


	//viewport controller
		private static IViewportController _viewportController;
		public static IViewportController viewportController
		{
			get	
			{
				_viewportController = ValidateController<IViewportController>(_viewportController)
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
				_inputController = ValidateController<IInputController>(_inputController);
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
				_toolManager = ValidateController<IToolManager>(_toolManager);
				return _toolManager;
			}
		}
	//ENDOF input controller	
	}
}