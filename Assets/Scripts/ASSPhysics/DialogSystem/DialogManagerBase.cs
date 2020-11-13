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
				if (waitingDialog != null) { return; }
				activeDialog.AnimatedDisable(DelegateDialogClosedCallback);
				waitingDialog = targetDialog;
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

			private void DelegateDialogClosedCallback ()
			{
				Debug.Log("Dialog closed callback");
				activeDialog = waitingDialog;
				if (waitingDialog == null) { return; }
				waitingDialog.Enable();
			}
		//ENDOF private method definition
	}
}