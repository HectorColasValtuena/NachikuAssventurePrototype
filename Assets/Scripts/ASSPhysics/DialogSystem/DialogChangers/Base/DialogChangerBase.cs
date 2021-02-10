using UnityEngine;

using IDialogController = ASSPhysics.DialogSystem.DialogControllers.IDialogController;
using TDialogManager = ASSPhysics.DialogSystem.DialogManagerBase;

namespace ASSPhysics.DialogSystem.DialogChangers
{
	public class DialogChangerBase : MonoBehaviour
	{
	//serialized fields
		[SerializeField]
		[Tooltip("IDialogController to activate on call")]
		protected ASSPhysics.DialogSystem.DialogControllers.DialogControllerSimple defaultTargetDialog = null;

		//if no dialog manager has been set try to find one in our parents or children
		[SerializeField]
		private TDialogManager _dialogManager = null;
		protected TDialogManager dialogManager
		{
			get
			{
				return (_dialogManager != null)
							? _dialogManager
							: transform.root.GetComponentInChildren<TDialogManager>();
			}
		}

		[SerializeField]
		private float delay = 0.0f;
	//ENDOF serialized fields

	//public methods
		//requests a dialogManager to activate target dialog
		public void ChangeDialog () { ChangeDialog(defaultTargetDialog); }
		public void ChangeDialog (IDialogController dialog)
		{
			//Debug.Log("Changing dialog");
			if (delay <= 0){ DoChangeDialog(dialog); }
			else { StartCoroutine(DelayedChangeDialog(dialog, delay)); }
		}

		private System.Collections.IEnumerator DelayedChangeDialog (IDialogController dialog, float delayLength)
		{
			yield return new UnityEngine.WaitForSeconds(delayLength);
			DoChangeDialog(dialog);
		}

		private void DoChangeDialog (IDialogController dialog)
		{
			dialogManager.SetActiveDialog(dialog);
		}
	//ENDOF public methods
	}
}