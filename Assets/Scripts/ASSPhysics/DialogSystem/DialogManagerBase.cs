using UnityEngine;

using IDialogController = ASSPhysics.DialogSystem.DialogControllers.IDialogController;
namespace ASSPhysics.DialogSystem
{
	public class DialogManagerBase : MonoBehaviour, IDialogManager
	{
		//static namespace
			public static DialogManagerBase instance;
		//ENDOF static namespace

		//serialized fields
		//ENDOF serialized fields

		//private fields and properties
			private IDialogController waitingDialog = null;
			private IDialogController activeDialog;
			//private int dialogIndex;
		//ENDOF private fields and properties

		//MonoBehaviour lifecycle
			public void Awake ()
			{
				instance = this;
				ResetDialogs();
			}
		//ENDOF MonoBehaviour lifecycle

		//IDialogManager implementation
			public void SetActiveDialog (IDialogController targetDialog)
			{
				//if already changing dialogs ignore change request
				if (waitingDialog != null) { return; }

				waitingDialog = targetDialog;
				//if there's another dialog active, request it closes
				if (activeDialog != null) 
				{
					activeDialog.AnimatedDisable(DelegateOpenNextDialog);
				}
				else	//if no dialog already active just open target dialog
				{
					DelegateOpenNextDialog();
				}

			}
		//ENDOF IDialogManager implementation

		//private method definition
			private void ResetDialogs ()
			{
				waitingDialog = null;
				activeDialog = null;
				IDialogController[] dialogList = GetComponentsInChildren<IDialogController>();
				foreach (IDialogController dialog in dialogList)
				{
					dialog.ForceDisable();
				}
			}

			private void DelegateOpenNextDialog ()
			{
				Debug.Log("Dialog closed callback");
				activeDialog = waitingDialog;
				if (waitingDialog == null) { return; }
				waitingDialog.Enable();
			}
		//ENDOF private method definition
	}
}