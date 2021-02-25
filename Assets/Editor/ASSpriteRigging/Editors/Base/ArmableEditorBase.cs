using UnityEngine;
using UnityEditor;

using IArmableInspector = ASSpriteRigging.Inspectors.IArmableInspector;

namespace ASSpriteRigging.Editors
{
	public abstract class ArmableEditorBase<TInspector>
	:
		EditorBase<TInspector>
		where TInspector : UnityEngine.Object, IArmableInspector
	{
	//protected properties
		protected bool isArmed 
		{ 
			get { return targetInspector.armed; }
			set { targetInspector.armed = value; }
		}
	//ENDOF protected properties

	//protected methods
		//forces inspector to disarm
		protected void Disarm ()
		{
			isArmed = false;
			Debug.Log("Disarmed");
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
	//ENDOF protected methods
	}
}