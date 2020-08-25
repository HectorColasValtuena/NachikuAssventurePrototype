using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ASSPriteRigging.BoneUtility;

namespace ASSPriteRigging
{
	[CustomEditor(typeof(SpriteSkinRigger))]
	public class SpriteSkinRiggerEditor : Editor
	{

		private SpriteSkinRigger spriteSkinRigger;

	//Setup button layout
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
			if (GUILayout.RepeatButton("Generate Sprite Bones"))
			{
				GenerateSpriteBones();
			}
		}
	//ENDOF Setup button layout

		private void GenerateSpriteBones ()
		{
			Debug.LogWarning("Generating sprite bones");
			//=================================================================================================================================
			//[TO-DO]
			//=================================================================================================================================
		}

		//incorporate every child without an ignore tag to our bone list
		private void ChildrenToBoneList ()
		{
			List<Transform> childList = new List<Transform>();

			Debug.Log(spriteSkinRigger.transform.name + " child count: " + spriteSkinRigger.transform.childCount);

			for (int i = 0, iLimit = spriteSkinRigger.transform.childCount; i < iLimit; i++)
			{
				Transform child = spriteSkinRigger.transform.GetChild(i);

				Debug.Log(i + ": " + BoneNomenclature.GetParameterList(child) + " > " + BoneNomenclature.IsIgnored(child));

				//add this child to the list if it doesn't contain an ignore tag
				if (!BoneNomenclature.IsIgnored(child))
				{
					childList.Add(child);
				}
			}

			spriteSkinRigger.boneList = childList.ToArray();
			Debug.Log("added children: " + childList.Count);
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