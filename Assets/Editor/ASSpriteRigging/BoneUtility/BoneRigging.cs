//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Component.ApplySettings(sample) methods for several UnityEngine components
using ASSistant.ComponentConfigurers;


namespace ASSpriteRigging.BoneUtility
{
	public static class BoneRigging
	{
		public static void RigBoneList (SpriteSkinRigger target)
		{ RigBoneList(target.spriteSkin.boneTransforms, target.defaultRigidbody, target.defaultAnchor, target.defaultSpring, target.defaultCollider, target.defaultTag, target.defaultLayer); }
		public static void RigBoneList (Transform[] boneList, Rigidbody2D defaultRigidbody, SpringJoint2D defaultAnchor, SpringJoint2D defaultSpring, CircleCollider2D defaultCollider, string defaultTag = null, int defaultLayer = -1)
		{
			foreach (Transform bone in boneList)
			{
				RigBoneIndividualElements (
					bone,
					defaultRigidbody: defaultRigidbody,
					defaultAnchor: defaultAnchor,
					defaultCollider: defaultCollider,
					defaultTag: defaultTag,
					defaultLayer: defaultLayer
				);
			}
		}

		//creates components that affect a single bone: rigidbodies and disconnected anchors/joints
		public static void RigBoneIndividualElements (Transform bone, Rigidbody2D defaultRigidbody, SpringJoint2D defaultAnchor, CircleCollider2D defaultCollider, string defaultTag = null, int defaultLayer = -1)
		{
			BoneCreateRigidbody(bone, defaultRigidbody);
			BoneCreateAnchor(bone, defaultAnchor);
			BoneCreateCollider(bone, defaultCollider);
			BoneSetTagAndLayer(bone, defaultTag, defaultLayer);
		}

		private static void BoneSetTagAndLayer (Transform bone, string targetTag, int targetLayer)
		{
			if (targetTag != null) { bone.gameObject.tag = targetTag; }
			if (targetLayer >= 0) { bone.gameObject.layer = targetLayer; }
		}

//======================================================================================================================
//[TO-DO] Condense the following methods into a single, generic-typed method BoneAddComponent<T>(T sample)
//======================================================================================================================
		//creates a rigidbody on target bone (if non existent) and sets its properties replicating sample
		private static void BoneCreateRigidbody (Transform bone, Rigidbody2D sample)
		{
			//create a rigidbody if it doesn't exist
			Rigidbody2D newRigidbody = bone.gameObject.GetComponent<Rigidbody2D>();
			if (newRigidbody == null) { newRigidbody = ObjectFactory.AddComponent<Rigidbody2D>(bone.gameObject); }
			//now configure new rigidbody
			newRigidbody.ApplySettings(sample);
		}

		//creates a CircleCollider2D on target bone (if non existent) and sets its properties replicating sample
		private static void BoneCreateCollider (Transform bone, CircleCollider2D sample)
		{
			//create a rigidbody if it doesn't exist
			CircleCollider2D newCollider = bone.gameObject.GetComponent<CircleCollider2D>();
			if (newCollider == null) { newCollider = ObjectFactory.AddComponent<CircleCollider2D>(bone.gameObject); }
			//now configure new rigidbody
			newCollider.ApplySettings(sample);
		}

		//creates a parent-anchored joint on bone target (if non existent) and sets its properties replicating sample
		private static void BoneCreateAnchor (Transform bone, SpringJoint2D sample)
		{
			//try to find a joint pointing to the parent object.
			SpringJoint2D newAnchor = BoneRigging.BoneFindSpringConnected(bone, bone.parent);
			//if no pre-existent anchor found, create a new parent-connected spring
			if (newAnchor == null)
			{
				newAnchor = ObjectFactory.AddComponent<SpringJoint2D>(bone.gameObject);
				newAnchor.connectedBody = bone.parent.gameObject.GetComponent<Rigidbody2D>();
			}
			//apply sample settings
			newAnchor.ApplySettings(sample, false);
		}

//======================================================================================================================
//[TO-DO] should move everything from here onwards elsewhere
//======================================================================================================================

		//finds a SpringJoint2D connected to target. returns null if non-existant
		private static SpringJoint2D BoneFindSpringConnected (Transform bone, Transform target) { return BoneRigging.BoneFindSpringConnected(bone, target.gameObject.GetComponent<Rigidbody2D>()); }
		private static SpringJoint2D BoneFindSpringConnected (Transform bone, Rigidbody2D target)
		{
			//get a list of springs
			SpringJoint2D[] springList = bone.gameObject.GetComponents<SpringJoint2D>();
			foreach (SpringJoint2D spring in springList)
			{
				//find a spring connected to target rigidbody and return it
				if (spring.connectedBody == target)
				{
					return spring;
				}
			}
			return null;	//return null if none found
		}
	}
}