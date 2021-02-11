namespace ASSPhysics.DialogSystem.DialogControllers
{
	public interface IDialogController
	{
		//enable the dialog
		void Enable ();

		//disable the dialog and execute finishingcallback after done
		void AnimatedDisable (DParameterlessDelegate finishingCallback);

		//disable the dialog immediately
		void ForceDisable ();
	}

	public delegate void DParameterlessDelegate ();	
}