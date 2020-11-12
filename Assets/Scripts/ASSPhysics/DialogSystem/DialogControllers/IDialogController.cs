namespace ASSPhysics.DialogSystem.DialogControllers
{
	public interface IDialogController
	{
		void Enable ();
		void AnimatedDisable (DParameterlessDelegate finishingCallback);
		void ForceDisable ();	
	}

	public delegate void DParameterlessDelegate ();	
}