﻿using UnityEngine;

using IEnumerator = System.Collections.IEnumerator;

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
			}

			public void Start ()
			{
				ResetDialogs();
			}
		//ENDOF MonoBehaviour lifecycle

		//IDialogManager implementation
			public void SetActiveDialog (IDialogController targetDialog)
			{
				Debug.Log("DialogManagerBase.SetActiveDialog()");
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
				StartCoroutine(DelegateOpenNextDialogCoroutine());

				IEnumerator DelegateOpenNextDialogCoroutine ()
				{
					//one-frame delay introduced to guarantee next dialog's animator has a chance to resize the panel before frame
					yield return new WaitForEndOfFrame();

					activeDialog = waitingDialog;
					if (waitingDialog != null)
					{
						waitingDialog.Enable();
						waitingDialog = null;
					}
				}
			}
		//ENDOF private method definition
	}
}