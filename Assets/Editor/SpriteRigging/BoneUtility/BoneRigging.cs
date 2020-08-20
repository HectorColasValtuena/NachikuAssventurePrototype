//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SpriteRigging.BoneUtility
{
	public static class BoneRigging
	{
		public static void RigBoneList (Transform[] boneList, Rigidbody2D defaultRigidbody, SpringJoint2D defaultAnchor)
		{
			foreach (Transform bone in boneList)
			{
				BoneRigging.RigBone(bone, defaultRigidbody, defaultAnchor);
			}
		}

		public static void RigBone (Transform bone, Rigidbody2D defaultRigidbody, SpringJoint2D defaultAnchor)
		{
			//ignore ignored bones
			if (BoneNomenclature.IsIgnored(bone)) return;

			//first create rigidbody
			if (BoneNomenclature.RequiresRigidbody(bone)) { BoneRigging.BoneCreateRigidbody(bone, defaultRigidbody); }

			//then add required anchor
			if (BoneNomenclature.RequiresAnchor(bone)) { BoneRigging.BoneCreateAnchor(bone, defaultAnchor); }

			//Lastly add required springs
			//==============================================================================================================================
			//[TO-DO]
			//==============================================================================================================================
		}

		private static void BoneCreateRigidbody (Transform bone, Rigidbody2D sample)
		{
			//create a rigidbody if it doesn't exist
			Rigidbody2D newRigidbody = bone.gameObject.GetComponent<Rigidbody2D>();
			if (newRigidbody == null) { newRigidbody = ObjectFactory.AddComponent<Rigidbody2D>(bone.gameObject); }
			//now configure new rigidbody
			RigidbodyApplySettings(newRigidbody, sample);
		}

		private static void BoneCreateAnchor (Transform bone, SpringJoint2D sample)
		{
			//==============================================================================================================================
			//[TO-DO]
			//==============================================================================================================================

		}

		private static bool BoneHasAnchor (Transform bone)
		{
			//==============================================================================================================================
			//[TO-DO]
			//==============================================================================================================================
			return true;
		}

		//applies right-hand properties to left-hand object
		private static void RigidbodyApplySettings (Rigidbody2D target, Rigidbody2D sample)
		{
			target.bodyType = 				sample.bodyType;				//body type
			target.sharedMaterial = 		sample.sharedMaterial;			//material
			target.simulated = 				sample.simulated;				//simulated
			target.mass = 					sample.mass;					//mass
			target.useAutoMass = 			sample.useAutoMass;				//use auto mass
			target.drag = 					sample.drag;					//linear drag
			target.angularDrag = 			sample.angularDrag;				//angular drag
			target.gravityScale = 			sample.gravityScale;			//gravity scale
			target.collisionDetectionMode = sample.collisionDetectionMode;	//collision detection
			target.sleepMode = 				sample.sleepMode;				//sleeping mode
			target.interpolation = 			sample.interpolation;			//interpolate
			target.constraints = 			sample.constraints;				//constraints (freeze position X, Y, freeze rotation Z)
		}
	}
}