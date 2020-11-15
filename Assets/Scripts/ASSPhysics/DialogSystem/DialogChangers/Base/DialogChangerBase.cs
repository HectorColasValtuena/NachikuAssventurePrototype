using UnityEngine;

using TDialogController = ASSPhysics.DialogSystem.DialogControllers.DialogControllerBase;
using TDialogManager = ASSPhysics.DialogSystem.DialogManagerBase;

namespace ASSPhysics.DialogSystem.DialogChangers
{
	public class DialogChangerBase : MonoBehaviour
	{
	//serialized fields
		[SerializeField]
		[Tooltip("IDialogController to activate on call")]
		protected TDialogController defaultTargetDialog = null;

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
		public void ChangeDialog (TDialogController dialog)
		{
			//Debug.Log("Changing dialog");
			if (delay <= 0){ DoChangeDialog(dialog); }
			else { StartCoroutine(DelayedChangeDialog(dialog, delay)); }
		}

		private System.Collections.IEnumerator DelayedChangeDialog (TDialogController dialog, float delayLength)
		{
			yield return new UnityEngine.WaitForSeconds(delayLength);
			DoChangeDialog(dialog);
		}

		private void DoChangeDialog (TDialogController dialog)
		{
			dialogManager.SetActiveDialog(dialog);
		}
	//ENDOF public methods
	}
}