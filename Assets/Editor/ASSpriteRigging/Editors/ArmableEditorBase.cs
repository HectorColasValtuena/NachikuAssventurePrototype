using UnityEngine;
using UnityEditor;

using ArmableInspectorBase = ASSpriteRigging.BaseInspectors.ArmableInspectorBase; 

namespace ASSpriteRigging.Editors
{
	public abstract class ArmableEditorBase : Editor
	{
		protected bool isArmed { get { return (target as ArmableInspectorBase).armed; } set { (target as ArmableInspectorBase).armed = value; }}

	//Setup GUI layout
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI();

			InspectorInitialization();
			DoButtons();
		}

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
	//ENDOF Setup GUI layout

	//overridable methods and properties
		protected abstract void InspectorInitialization ();
		protected abstract void DoButtons ();
	//ENDOF overridable methods
	}
}