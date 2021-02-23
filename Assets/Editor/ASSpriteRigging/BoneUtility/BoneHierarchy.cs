using UnityEngine;
using UnityEditor;

using UnityEngine.U2D.Animation; //SpriteSkin
using U2DAnimationAccessor;	//SpriteSkin.

using IRiggerInspector = ASSpriteRigging.Inspectors.IRiggerInspector;

namespace ASSpriteRigging.BoneUtility
{
	public static class BoneHierarchy
	{
		//finds a joint of type TJoint connected to target transform or rigidbody.
		//returns null if target is not connected or non-existant
		public static TJoint BoneFindJointConnected <TJoint> (Transform bone, Transform target)
			where TJoint: Joint
		{
			Rigidbody targetRigidbody = target.gameObject.GetComponent<Rigidbody>();
			if (targetRigidbody == null) { return null; }
			return BoneFindJointConnected<TJoint> (bone, targetRigidbody);
		}
		public static TJoint BoneFindJointConnected <TJoint> (Transform bone, Rigidbody targetRigidbody)
			where TJoint: Joint
		{
			//get a list of all the joints of type TJoint contained in the origin bone
			TJoint[] jointList = bone.gameObject.GetComponents<TJoint>();
			//find a joint connected to target rigidbody and return it
			foreach (TJoint joint in jointList)
			{
				if (joint.connectedBody == targetRigidbody)
				{
					return joint;
				}
			}
			return null;//return null if none found
		}

		//creates gameobjects for every bone and stores them in spriteskin
		public static void CreateBoneHierarchy (IRiggerInspector rigger)
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