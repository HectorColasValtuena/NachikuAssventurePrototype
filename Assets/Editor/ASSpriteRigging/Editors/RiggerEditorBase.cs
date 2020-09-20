using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ASSpriteRigging.BoneUtility;

namespace ASSpriteRigging.Editors
{
	public abstract class RiggerEditorBase : Editor
	{
	//Setup GUI layout
		protected abstract bool isArmed {get; set;}

		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI();
			InspectorInitialization();
			DoFullSetupButton();
			DoRigBoneListButton();
		}

		//check if script is armed for use
		private bool RequestArmed ()
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

		public void DoFullSetupButton ()
		{
			if (GUILayout.Button("Full setup"))
			{
				if (RequestArmed()) { FullSetup(); }
			}
		}

		public void DoRigBoneListButton ()
		{
			if (GUILayout.Button("Setup & configure bone components"))//, GUILayout.MaxWidth(125f)))
			{
				if (RequestArmed()) { RigBones(); }
			}
		}
	//ENDOF Setup GUI layout

	//abstract methods
		protected abstract void InspectorInitialization ();
		protected abstract void FullSetup ();
		protected abstract void RigBones ();
	//ENDOF abstract methods
	}
}