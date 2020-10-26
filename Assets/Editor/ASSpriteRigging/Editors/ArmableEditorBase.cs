using UnityEngine;
using UnityEditor;

using ArmableInspectorBase = ASSpriteRigging.BaseInspectors.ArmableInspectorBase; 

namespace ASSpriteRigging.Editors
{
	public abstract class ArmableEditorBase<TInspector> : EditorBase<TInspector>
		where TInspector : ArmableInspectorBase
	{
		protected bool isArmed 
		{ 
			get { return (targetInspector as ArmableInspectorBase).armed; }
			set { (targetInspector as ArmableInspectorBase).armed = value; }
		}

	//Setup GUI layout
		//check if script is armed for use
		protected bool RequestArmed ()
		{
			if (isArmed)
			{
				isArmed = false;
				return true;
			}
			else
			{
				Debug.LogWarning("Rigger is disarmed - Arm before proceeding");
				return false;
			}
		}

		//draws a button that executes its corresponding action only if armed
		protected override void DoButton (string buttonText, EditorActionDelegate action)
		{
			base.DoButton(buttonText, delegate() {
				if (RequestArmed()) { action(); }
			});
		}
	//ENDOF Setup GUI layout
	}
}