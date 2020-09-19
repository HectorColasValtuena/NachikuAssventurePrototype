using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ASSpriteRigging.BoneUtility;

namespace ASSpriteRigging.Editors
{
	[CustomEditor(typeof(SpriteSkinRigger))]
	public class SpriteSkinRiggerEditor : Editor
	{

		private SpriteSkinRigger spriteSkinRigger;

	//Setup GUI layout
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI();

			spriteSkinRigger = (SpriteSkinRigger) target;

			DoFullSetupButton();
			DoRigBoneListButton();
			/*[DEBUG]*/DoIncognitoButton();
		}

		//check if script is armed for use
		private bool RequestArmed ()
		{
			if (spriteSkinRigger.armed)
			{
				spriteSkinRigger.armed = false;
				return true;
			}
			else
			{
				Debug.LogWarning("SpriteSkinRigger is disarmed - Arm before proceeding");
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
				if (RequestArmed()) { SpriteSkinRigging.RigBones(spriteSkinRigger); }
			}
		}

		/*[DEBUG]*/
		public void DoIncognitoButton ()
		{
			if (GUILayout.Button("Incognito Button"))//, GUILayout.MaxWidth(125f)))
			{
				Debug.Log("Don't mind me, I'm just Incognito Button");
				Debug.Log(spriteSkinRigger.spriteSkin.boneTransforms);
			}
		}
		/*[DEBUG]*/

	//ENDOF Setup GUI layout
		//performs every step of the automated rigging process at once:
		//moves ten units of sperm forwards, then cast whale at next 2 tiles unless hitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitler
		//that means:
			//> create gameobjects for every bone
			//> rig default components for every corresponding bone gameobject
		private void FullSetup ()
		{
			Debug.Log("Initiating full setup of " + target.name);
			BoneHierarchy.CreateBoneHierarchy(spriteSkinRigger);
			SpriteSkinRigging.RigBones(spriteSkinRigger);
		}
	}
}