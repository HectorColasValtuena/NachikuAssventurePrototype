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

			DoGenerateSpriteBonesButton();
			/*[DEBUG]*/DoIncognitoButton();
			DoChildrenToBoneListButton();
			DoRigBoneListButton();
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

		public void DoChildrenToBoneListButton ()
		{
			if (GUILayout.Button("Children to Bone List"))//, GUILayout.MaxWidth(125f)))
			{
				Debug.Log("Children to bone list");
				ChildrenToBoneList();
			}
		}

		public void DoRigBoneListButton ()
		{
			if (GUILayout.Button("Rig bone list"))//, GUILayout.MaxWidth(125f)))
			{
				Debug.Log("Rigging children of " + target.name);
				RigBoneList();
				Debug.Log("==== Done rigging");
			}
		}

		public void DoGenerateSpriteBonesButton ()
		{
			if (GUILayout.Button("Generate Sprite Bones"))
			{
				GenerateSpriteBones();
			}
		}
	//ENDOF Setup GUI layout

		//create bone information from the vertex list
		private void GenerateSpriteBones ()
		{
			spriteSkinRigger.targetSprite.BonesFromVertexList(spriteSkinRigger.baseBoneName);
			/*
			Sprite sprite = spriteSkinRigger?.gameObject.GetComponent<SpriteRenderer>()?.sprite;
			if (sprite != null) { sprite.BonesFromVertexList(spriteSkinRigger.baseBoneName); }
			else { Debug.LogWarning("No sprite found"); }
			*/
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
			BoneRigging.RigBoneList(spriteSkinRigger.boneList, spriteSkinRigger.defaultRigidbody, spriteSkinRigger.defaultAnchor);
		}
	}
}