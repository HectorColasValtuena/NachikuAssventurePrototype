﻿using UnityEngine;
using UnityEditor;

using Unity.Collections; //NativeArray<T>

using static ASSistant.ComponentConfiguration.ComponentConfigurerGeneric; //Component.EMApplySettings(sample);
using ASSpriteRigging.BoneUtility;	//BoneHierarchy.BoneFindJointConnected()

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
		public static TComponent BoneSetupComponent <TComponent> (Transform bone, TComponent sample)
			where TComponent: Component
		{
			return BoneSetupComponent<TComponent>(bone).EMApplySettings(sample);
		}
		public static TComponent BoneSetupComponent <TComponent> (Transform bone)
			where TComponent: Component
		{
			TComponent component = bone.gameObject.GetComponent<TComponent>();
			if (component == null) { component = ObjectFactory.AddComponent<TComponent>(bone.gameObject); }
			return component;
		}

		//Creates a joint from bone transform to target transform/rigidbody, and applies sample settings
		public static TJoint BoneConnectJoint <TJoint> (Transform bone, Transform target, TJoint sample)
			where TJoint: Joint
		{
			return BoneConnectJoint<TJoint> (bone, target.gameObject.GetComponent<Rigidbody>(), sample);
		}
		public static TJoint BoneConnectJoint <TJoint> (Transform bone, Rigidbody targetRigidbody, TJoint sample)
			where TJoint: Joint
		{
			//first try to find a pre-existing joint of adequate type and connected target
			TJoint joint = BoneHierarchy.BoneFindJointConnected<TJoint>(bone, targetRigidbody);

			//if desired joint did not exist, create a new joint
			if (joint == null)
			{
				joint = ObjectFactory.AddComponent<TJoint>(bone.gameObject);
			}

			//copy public properties from sample object, connect the joint to the target, and return it
			joint.EMApplySettings(sample);
			joint.connectedBody = targetRigidbody;
			return joint;
		}

		//find and remove spring joint connected to target
		public static void BoneRemoveConnectedJoint <TJoint> (Transform bone, Transform connectedTarget)
			where TJoint: Joint
		{
			TJoint foundJoint = BoneHierarchy.BoneFindJointConnected<TJoint>(bone, connectedTarget);
			if (foundJoint != null) 
			{
				Object.DestroyImmediate(foundJoint);
			}
		}

		//creates a spring connecting both bones. If mutual, creates a spring from each bone, only from the first otherwise
		public static void InterconnectBonePair <TJoint> (Transform bone1, Transform bone2, TJoint sample, bool mutual = false)
			where TJoint: Joint
		{
			if (bone1 == bone2) { return; }

			BoneConnectJoint<TJoint>(bone1, bone2, sample);

			//if connection is mutual create the opposite joint. if not, ensure there is no opposite joint
			if (mutual) { BoneConnectJoint<TJoint>(bone2, bone1, sample); }
			else { BoneRemoveConnectedJoint<TJoint>(bone2, bone1); }
		}
	}
}