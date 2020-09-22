﻿using UnityEngine;
using UnityEditor;

using Unity.Collections; //NativeArray<T>

using ASSistant.ComponentConfigurers; //Component.ApplySettings() extension methods for several UnityEngine components
using ASSpriteRigging.BoneUtility;	//BoneFindSpringConnected()

namespace ASSpriteRigging.BoneUtility
{
	public static class BoneRigging
	{
		public static void BoneSetTagAndLayer (Transform bone, string targetTag, int targetLayer)
		{
			if (targetTag != null) { bone.gameObject.tag = targetTag; }
			if (targetLayer >= 0) { bone.gameObject.layer = targetLayer; }
		}

		//Ensures bone Transform contains one component of type T and applies sample settings if received
		public static T BoneSetupComponent <T> (Transform bone, T sample) where T: Component
		{
			//return ComponentConfigurers.ApplySettings(BoneSetupComponent<T>(bone), sample);
			return BoneSetupComponent<T>(bone).ApplySettings(sample);
		}
		public static T BoneSetupComponent <T> (Transform bone) where T: Component
		{
			T component = bone.gameObject.GetComponent<T>();
			if (component == null) { component = ObjectFactory.AddComponent<T>(bone.gameObject); }
			return component;
		}

//======================================================================================================================
//[TO-DO] Condense the following methods into a single, generic-typed method BoneAddComponent<T>(T sample)
//======================================================================================================================
		//creates a rigidbody on target bone (if non existent) and sets its properties replicating sample
		public static void BoneCreateRigidbody (Transform bone, Rigidbody2D sample)
		{
			Debug.LogWarning("BoneRigging old create component is deprecated - remove me");
			//create a rigidbody if it doesn't exist
			Rigidbody2D newRigidbody = bone.gameObject.GetComponent<Rigidbody2D>();
			if (newRigidbody == null) { newRigidbody = ObjectFactory.AddComponent<Rigidbody2D>(bone.gameObject); }
			//now configure new rigidbody
			newRigidbody.ApplySettings(sample);
		}

		//creates a CircleCollider2D on target bone (if non existent) and sets its properties replicating sample
		public static void BoneCreateCollider (Transform bone, CircleCollider2D sample)
		{
			Debug.LogWarning("BoneRigging old create component is deprecated - remove me");
			//create a rigidbody if it doesn't exist
			CircleCollider2D newCollider = bone.gameObject.GetComponent<CircleCollider2D>();
			if (newCollider == null) { newCollider = ObjectFactory.AddComponent<CircleCollider2D>(bone.gameObject); }
			//now configure new rigidbody
			newCollider.ApplySettings(sample);
		}

		//creates a parent-anchored joint on bone target (if non existent) and sets its properties replicating sample
		public static void BoneCreateAnchor (Transform bone, SpringJoint2D sample)
		{
			BoneCreateSpringJoint (bone, bone.parent, sample);
		}

		//create a spring joint pointing to target
		public static void BoneCreateSpringJoint (Transform bone, Transform target, SpringJoint2D sample)
		{
			Debug.LogWarning("BoneRigging old create component is deprecated - remove me");
			Rigidbody2D targetRigidbody = target.gameObject.GetComponent<Rigidbody2D>();
			//try to find a pre-existing joint pointing to target object.
			SpringJoint2D newJoint = BoneHierarchy.BoneFindSpringConnected(bone, targetRigidbody);
			//if no pre-existent anchor found, create a new joint
			if (newJoint == null)
			{
				newJoint = ObjectFactory.AddComponent<SpringJoint2D>(bone.gameObject);
			}

			//apply joint settings
			newJoint.ApplySettings(sample, false);
			newJoint.connectedBody = targetRigidbody;
		}

		//find and remove spring joint connected to target
		private static void BoneRemoveSpringJoint (Transform bone, Transform connectedTarget)
		{
			SpringJoint2D foundJoint = BoneHierarchy.BoneFindSpringConnected(bone, connectedTarget);
			if (foundJoint != null) 
			{
				Object.DestroyImmediate(foundJoint);
			}
		}

	//Generate springs between bones
		public static void RigBoneSpringMesh (Transform[] boneList, NativeArray<ushort> triangleList, SpringJoint2D sample)
		{
			if ((triangleList.Length % 3) != 0) { Debug.LogWarning("RigBoneSpringMesh() triangles.Length is not a multiple of 3"); }
			for (int i = 0, iLimit = triangleList.Length; i < iLimit; i += 3)
			{
				//for every 3 vertex entries, process them as a triangle
				BoneGenerateSpringPolygon (boneList, triangleList.GetSubArray(i, 3), sample);
				//BoneGenerateSpringPolygon (boneList, triangleList.GetSubArray(i, Mathf.Min(i + 2, iLimit - 1)), sample);
			}
		}

		//Generates the springs for a single polygon
		private static void BoneGenerateSpringPolygon (Transform[] boneList, NativeArray<ushort> polygon, SpringJoint2D sample)
		{
			//first element will connect to the last enclosing the polygon
			int previousBone = polygon.Length - 1;
			//connect every bone to the previous bone in the polygon
			for (int i = 0, iLimit = polygon.Length; i < iLimit; i++)
			{
				BonesCreateSpringInterconnection (
					bone1: boneList[polygon[i]],
					bone2: boneList[polygon[previousBone]],
					sample: sample
				);
				previousBone = i;
			}
		}

		//creates a spring connecting both bones. If mutual, creates a spring from each bone, only from the first otherwise
		private static void BonesCreateSpringInterconnection (Transform bone1, Transform bone2, SpringJoint2D sample, bool mutual = false)
		{
			if (bone1 == bone2) { return; }

			BoneCreateSpringJoint(bone1, bone2, sample);

			//if connection is mutual create the opposite joint. if not, ensure there is no opposite joint
			if (mutual) { BoneCreateSpringJoint(bone2, bone1, sample); }
			else { BoneRemoveSpringJoint(bone2, bone1); }
		}

		public static void AddComponentToTransform <T> (Transform transform) where T : MonoBehaviour
		{
			if (transform.GetComponent<T>() == null)
			{
				ObjectFactory.AddComponent<T>(transform.gameObject);
			}
		}

		public static void BoneCreateFixedJoint (Transform bone, Transform target, SpringJoint2D sample)
		{
			Rigidbody2D targetRigidbody = target.gameObject.GetComponent<Rigidbody2D>();
			//try to find a pre-existing joint pointing to target object.
			SpringJoint2D newJoint = BoneHierarchy.BoneFindSpringConnected(bone, targetRigidbody);
			//if no pre-existent anchor found, create a new joint
			if (newJoint == null)
			{
				newJoint = ObjectFactory.AddComponent<SpringJoint2D>(bone.gameObject);
			}

			//apply joint settings
			newJoint.ApplySettings(sample, false);
			newJoint.connectedBody = targetRigidbody;
		}
	//ENDOF Generate springs for bones		
	}
}