using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.U2D.Animation; //SpriteSkin
using U2DAnimationAccessor;	//SpriteSkin.

namespace ASSpriteRigging.BoneUtility
{
	public static class BoneHierarchy
	{
		public static List<Transform> GetChildren (Transform root, bool recursive = true, bool includeIgnored = false)
		{
		/*
		===========================================================================================================
			Deprecated: replace with a method fetching the list from the spriteSkin object
		===========================================================================================================
		*/
			Debug.LogWarning("BoneHierarchy.GetChildren() deprecated version called (wants their stuff back)");
			List<Transform> childList = new List<Transform>();
			for (int i = 0, iLimit = root.childCount; i < iLimit; i++)
			{
				Transform child = root.GetChild(i);

				//ignore this element if it includes an ignore tag
				if (!includeIgnored && BoneNomenclature.IsIgnored(child)) { continue; }

				childList.Add(child);

				//add children of this element if necessary
				if (recursive) { childList.AddRange(GetChildren(child, recursive, includeIgnored));	}
			}
			return childList;
		}

		//creates gameobjects for every bone and stores them in spriteskin
		public static void CreateBoneHierarchy (SpriteSkinRigger spriteSkinRigger)
		{
			SpriteSkin spriteSkin = spriteSkinRigger.spriteSkin;
			Sprite sprite = spriteSkinRigger.targetSprite;

			if (sprite == null || spriteSkin.rootBone != null)
			{
				Debug.LogError("No sprite or no rootBone @" + spriteSkin.gameObject.name);
				return;
			}

			Undo.RegisterCompleteObjectUndo(spriteSkin, "Create Bones");

			//call accessor-exposed CreateBoneHierarchy method on the sprite skin
			//this is what creates the transform structure
			spriteSkin.PublicCreateBoneHierarchy();

			foreach (Transform transform in spriteSkin.boneTransforms) 
			{
				Undo.RegisterCreatedObjectUndo(transform.gameObject, "Create Bones");
			}

			//reset bounds if needed
			spriteSkin.CalculateBoundsIfNecessary();

			EditorUtility.SetDirty(spriteSkin);
		}
	}
}