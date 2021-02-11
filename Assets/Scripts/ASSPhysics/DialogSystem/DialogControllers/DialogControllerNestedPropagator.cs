using UnityEngine;

using System.Collections.Generic;
using IEnumerator = System.Collections.IEnumerator;

namespace ASSPhysics.DialogSystem.DialogControllers
{
	public class DialogControllerNestedPropagator : DialogControllerBase
	{
	//private fields and properties
		private IDialogController[] childDialogArray = null;

		private int closuresLeft = 0; //number of children left to close
	//ENDOF private fields and properties

	//IDialogController inherited overrides
		public override void Enable ()
		{
			base.Enable();

			foreach (IDialogController dialog in childDialogArray)
			{
				dialog.Enable();
			}
		}

		protected override void PerformClosure ()
		{
			closuresLeft = childDialogArray.Length;

			foreach (IDialogController dialog in childDialogArray)
			{
				dialog.AnimatedDisable(DelegateChildFinishedClosing);
			}
		}

		public override void ForceDisable ()
		{
			foreach (IDialogController dialog in childDialogArray)
			{
				dialog.ForceDisable();
			}

			base.ForceDisable();
		}
	//ENDOF IDialogController inherited overrides

	//MonoBehaviour lifecycle implementation	
		public void Awake ()
		{
			if (childDialogArray == null || childDialogArray.Length == 0)
			{
				AutoInitializeChildList();
			}
		}
	//ENDOF MonoBehaviour lifecycle implementation

	//private method implementation
		private void AutoInitializeChildList ()
		{
			List<IDialogController> foundDialogList = new List<IDialogController>();
			for (int i = 0, iLimit = transform.childCount; i < iLimit; i++)
			{
				IDialogController foundDialog = transform.GetChild(i).GetComponent<IDialogController>();
				if (foundDialog != null)
				{
					foundDialogList.Add(foundDialog);
				}
			}

			childDialogArray = foundDialogList.ToArray();
		}

		private void DelegateChildFinishedClosing ()
		{
			closuresLeft--;
			if (closuresLeft <= 0)
			{
				FinishedClosingChildren();
			}
		}

		private void FinishedClosingChildren ()
		{
			base.ForceDisable();	//invoke the base version of forcedisable to avoid propagating forceDisable calls twice
			InvokeFinishingCallback();
		}
	//ENDOF private method implementation
	}
}