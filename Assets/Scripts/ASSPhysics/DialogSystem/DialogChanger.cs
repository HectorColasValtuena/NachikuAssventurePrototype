using UnityEngine;

using TDialogController = ASSPhysics.DialogSystem.DialogControllers.DialogControllerBase;
using TDialogManager = ASSPhysics.DialogSystem.DialogManagerBase;

namespace ASSPhysics.DialogSystem
{
	public class DialogChanger : MonoBehaviour
	{
	//serialized fields
		[SerializeField]
		[Tooltip("IDialogController to activate on call")]
		private TDialogController defaultTargetDialog;

		//if no dialog manager has been set try to find one in our parents or children
		[SerializeField]
		private TDialogManager _dialogManager;
		private TDialogManager dialogManager
		{
			get
			{
				return (_dialogManager != null)
							? _dialogManager
							: transform.root.GetComponentInChildren<TDialogManager>();
			}
		}
	//ENDOF serialized fields

	//public methods
		//requests a dialogManager to activate target dialog
		public void ChangeDialog () { ChangeDialog(defaultTargetDialog); }
		public void ChangeDialog (TDialogController dialog)
		{
			dialogManager.SetActiveDialog(dialog);
		}
	//ENDOF public methods
	}
}