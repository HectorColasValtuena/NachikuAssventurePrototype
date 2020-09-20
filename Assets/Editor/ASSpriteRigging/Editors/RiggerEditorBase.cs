using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ASSpriteRigging.BoneUtility;
using ASSpriteRigging.Riggers; //SpriteSkinBaseRigger

namespace ASSpriteRigging.Editors
{
	public abstract class RiggerEditorBase : Editor
	{
		protected SpriteSkinBaseRigger rigger;
		protected bool isArmed { get { return rigger.armed; } set { rigger.armed = value; }}

	//Setup GUI layout
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI();
			rigger = (SpriteSkinBaseRigger) target;

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

	//Core common logic
		//performs every step of the automated rigging process at once:
		//moves ten units of sperm forwards, then cast whale at next 2 tiles unless hitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitler
		//that means:
			//> create gameobjects for every bone invoking the corresponding SpriteSkin methods
			//> rig default components for every corresponding bone gameobject (abstract- each rigger performs its own rigging)

		protected void FullSetup ()
		{
			Debug.Log("Initiating full setup of " + target.name);
			BoneHierarchy.CreateBoneHierarchy(rigger);
			RigBones();
			Debug.Log("Full setup finished");
		}
	//ENDOF Core common logic

	//overridable methods and properties
		protected virtual void InspectorInitialization () {}
		protected abstract void RigBones ();
	//ENDOF overridable methods
	}
}