using ServiceContainer = System.ComponentModel.Design.ServiceContainer;

namespace ASSPhysics.ControllerSystem
{
	public static class ControllerProvider
	{
	//private fields and properties
		private static ServiceContainer serviceContainer;

	//ENDOF private fields and properties


	//public methods
		public static TController GetController <TController> ()
		{
			return (TController) serviceContainer?.GetService(typeof(TController));
		}

		public static void RegisterController <TController> (TController controller)
		{
			InitializeContainer();

			if (GetController<TController>() != null)
			{
				DisposeController<TController>();
			}

			serviceContainer.AddService(typeof(TController), controller);
		}

		public static void DisposeController <TController> ()
		{
			serviceContainer.RemoveService(typeof(TController));
		}
	//ENDOF public methods

	//private methods
		//ensure a container exists
		private static void InitializeContainer ()
		{
			if (serviceContainer == null)
			{ serviceContainer = new ServiceContainer(); }
		}
	//ENDOF private methods
	}
}