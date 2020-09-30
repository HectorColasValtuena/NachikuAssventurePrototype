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
	}
}