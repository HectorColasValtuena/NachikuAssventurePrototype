using IDialogController = ASSPhysics.DialogSystem.DialogControllers.IDialogController;

namespace ASSPhysics.DialogSystem
{
	public interface IDialogManager
	{
		void SetActiveDialog (IDialogController targetDialog);
	}

}