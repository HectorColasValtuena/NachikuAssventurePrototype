//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ASSPriteRigging.BoneUtility
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

		//creates a rigidbody on bone target (if non existent) and sets its properties replicating sample
		private static void BoneCreateRigidbody (Transform bone, Rigidbody2D sample)
		{
			//create a rigidbody if it doesn't exist
			Rigidbody2D newRigidbody = bone.gameObject.GetComponent<Rigidbody2D>();
			if (newRigidbody == null) { newRigidbody = ObjectFactory.AddComponent<Rigidbody2D>(bone.gameObject); }
			//now configure new rigidbody
			RigidbodyApplySettings(newRigidbody, sample);
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
			AnchorApplySettings(newAnchor, sample, false);
		}

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

		//applies right-hand properties to left-hand object
		private static void AnchorApplySettings (SpringJoint2D target, SpringJoint2D sample, bool alterConnectedBody = false)
		{
			if (alterConnectedBody)	{ target.connectedBody = sample.connectedBody; }		//conneted rigidbody
			target.enableCollision = 				sample.enableCollision;					//enable collision
			target.autoConfigureConnectedAnchor = 	sample.autoConfigureConnectedAnchor;	//auto configure connection
			target.anchor = 						sample.anchor;							//anchor x y
			target.connectedAnchor = 				sample.connectedAnchor;					//connected anchor x y
			target.autoConfigureDistance = 			sample.autoConfigureDistance;			//auto configure distance
			target.distance = 						sample.distance;						//distance
			target.dampingRatio = 					sample.dampingRatio;					//Damping ratio
			target.frequency = 						sample.frequency;						//frequency
			target.breakForce = 					sample.breakForce;						//break force
		}
	}
}