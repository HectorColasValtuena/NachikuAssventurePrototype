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

			spriteSkinRigger.armed = false;

			DoFullSetupButton();
			DoRigBoneListButton();
			/*[DEBUG]*/DoIncognitoButton();
			//[DEPRECATED] DoChildrenToBoneListButton();
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
			if (GUILayout.Button("Rig bone list"))//, GUILayout.MaxWidth(125f)))
			{
				if (RequestArmed()) { RigBoneList(); }
			}
		}

		/*[DEBUG]*/
		public void DoIncognitoButton ()
		{
			if (GUILayout.Button("Incognito Button"))//, GUILayout.MaxWidth(125f)))
			{
				Debug.Log("Don't mind me, I'm just Incognito Button");
				DebugReportBoneList();
			}
		}
		/*[DEBUG]*/

		/*
		[DEPRECATED]
		public void DoChildrenToBoneListButton ()
		{
			if (GUILayout.Button("Children to Bone List"))//, GUILayout.MaxWidth(125f)))
			{
				if (RequestArmed())
				{
					Debug.Log("Children to bone list");
					ChildrenToBoneList();
				}
			}
		}
		*/
	//ENDOF Setup GUI layout
		//performs every step of the automated rigging process at once:
		//moves ten units of sperm forwards, then cast whale at next 2 tiles unless hitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitler
		//that means: create gameobjects for every bone, then rig default components for every corresponding bone gameobject
		private void FullSetup ()
		{
			Debug.Log("Initiating full setup of " + target.name);
			CreateBoneHierarchy();
		}

		//creates gameobjects for every bone and stores them in spriteskin
		private void CreateBoneHierarchy ()
		{
			var spriteSkin = spriteSkinRigger.spriteSkin;
			var sprite = spriteSkinRigger.targetSprite;

			if (sprite == null || spriteSkin.rootBone != null)
			{
				Debug.LogError("No sprite or no rootBone @" + spriteSkin.gameObject.name);
				return;
			}

			Undo.RegisterCompleteObjectUndo(spriteSkin, "Create Bones");

			spriteSkin.CreateBoneHierarchy();

			foreach (var transform in spriteSkin.boneTransforms) 
			{
				Undo.RegisterCreatedObjectUndo(transform.gameObject, "Create Bones");
			}

			//reset bounds if needed
			if (spriteSkin.isValid && spriteSkin.bounds == new Bounds())
			{
                spriteSkin.CalculateBounds();
			}

			EditorUtility.SetDirty(spriteSkin);
		}

		//incorporate every child without an ignore tag to our bone list
		private void ChildrenToBoneList ()
		{
			spriteSkinRigger.boneList = BoneUtility.BoneHierarchy.GetChildren(spriteSkinRigger.transform).ToArray();
		}

		private void DebugReportBoneList ()
		{
			Debug.Log("Listing bones in GO " + spriteSkinRigger.transform.name);
			Debug.Log("========");
			foreach (Transform bone in spriteSkinRigger.boneList)
			{
				Debug.Log(" GO " + bone.name);
				Debug.Log(" name: " + BoneNomenclature.GetProperName(bone));
				Debug.Log(
					(BoneNomenclature.IsIgnored(bone) ? " ignore" : "") +
					(BoneNomenclature.RequiresRigidbody(bone) ? " rigidbody" : "") +
					(BoneNomenclature.RequiresAnchor(bone) ? " anchor" : "")
				);

				string springList = " springs:";
				//list all the spring targets
				foreach (string springName in BoneNomenclature.GetSpringTargets(bone)) { springList += " " + springName; }

				Debug.Log(springList);
				Debug.Log("========");
			}
		}

		//Read bone list and populate it with components corresponding to its tags
		private void RigBoneList ()
		{
			Debug.Log("Rigging children of " + target.name);
			BoneRigging.RigBoneList(spriteSkinRigger.boneList, spriteSkinRigger.defaultRigidbody, spriteSkinRigger.defaultAnchor);
			Debug.Log("==== Done rigging");
		}
	}
}