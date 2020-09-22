using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.U2D.Animation; //SpriteSkin
using U2DAnimationAccessor;	//SpriteSkin.

using ASSpriteRigging.Riggers; //SpriteSkinBaseRigger

namespace ASSpriteRigging.BoneUtility
{
	public static class BoneHierarchy
	{
		//finds a joint of type Tjoint connected to target. returns null if non-existant
		public static TJoint2D BoneFindJointConnected <TJoint2D> (Transform bone, Transform target)
			where TJoint2D: Joint2D
		{
			return BoneFindJointConnected<TJoint2D> (bone, target.gameObject.GetComponent<Rigidbody2D>());
		}
		public static TJoint2D BoneFindJointConnected <TJoint2D> (Transform bone, Rigidbody2D targetRigidbody)
			where TJoint2D: joint2D
		{
			//get a list of all the joints of type TJoint2D contained in the origin bone
			TJoint2D[] jointList = bone.gameObject.GetComponents<TJoint2D>();
			//find a joint connected to target rigidbody and return it
			foreach (TJoint2D joint in jointList)
			{
				if (joint.connectedBody == targetRigidbody)
				{
					return joint;
				}
			}
			return null;//return null if none found
		}

		//creates gameobjects for every bone and stores them in spriteskin
		public static void CreateBoneHierarchy (SpriteSkinBaseRigger rigger)
		{
			SpriteSkin spriteSkin = rigger.spriteSkin;
			Sprite sprite = rigger.sprite;

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
	}
}