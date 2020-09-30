using UnityEngine;
using UnityEditor;

using Unity.Collections; //NativeArray<T>

using static ASSistant.ComponentConfiguration.ComponentConfigurer; //Component.ApplySettings(sample);
using ASSpriteRigging.BoneUtility;	//BoneHierarchy.BoneFindJointConnected()

namespace ASSpriteRigging.BoneUtility
{
	public static class BoneRigging2D
	{
		//Creates a joint from bone transform to target transform/rigidbody, and applies sample settings
		public static TJoint2D BoneConnectJoint2D <TJoint2D> (Transform bone, Transform target, TJoint2D sample)
			where TJoint2D: Joint2D
		{
			Rigidbody2D targetRigidbody = target.gameObject.GetComponent<Rigidbody2D>();
			if (targetRigidbody != null)
			{
				return BoneConnectJoint2D<TJoint2D> (bone, targetRigidbody, sample);
			}
			/*[DEBUG]*/Debug.LogWarning ("Connecting bone failed because target has no rigidbody: " + target.gameObject.name);
			return null;
		}
		public static TJoint2D BoneConnectJoint2D <TJoint2D> (Transform bone, Rigidbody2D targetRigidbody, TJoint2D sample)
			where TJoint2D: Joint2D
		{
			//first try to find a pre-existing joint of adequate type and connected target
			TJoint2D joint = BoneHierarchy2D.BoneFindJoint2DConnected<TJoint2D>(bone, targetRigidbody);

			//if desired joint did not exist, create a new joint
			if (joint == null)
			{
				joint = ObjectFactory.AddComponent<TJoint2D>(bone.gameObject);
			}

			//copy public properties from sample object, connect the joint to the target, and return it
			joint.ApplySettings(sample);
			joint.connectedBody = targetRigidbody;
			return joint;
		}

		//find and remove spring joint connected to target
		public static void BoneRemoveConnectedJoint2D <TJoint2D> (Transform bone, Transform connectedTarget)
			where TJoint2D: Joint2D
		{
			TJoint2D foundJoint = BoneHierarchy2D.BoneFindJoint2DConnected<TJoint2D>(bone, connectedTarget);
			if (foundJoint != null) 
			{
				Object.DestroyImmediate(foundJoint);
			}
		}

		//creates a spring connecting both bones. If mutual, creates a spring from each bone, only from the first otherwise
		public static void InterconnectBonePair2D <TJoint2D> (Transform bone1, Transform bone2, TJoint2D sample, bool mutual = false)
			where TJoint2D: Joint2D
		{
			if (bone1 == bone2) { return; }

			BoneConnectJoint2D<TJoint2D>(bone1, bone2, sample);

			//if connection is mutual create the opposite joint. if not, ensure there is no opposite joint
			if (mutual) { BoneConnectJoint2D<TJoint2D>(bone2, bone1, sample); }
			else { BoneRemoveConnectedJoint2D<TJoint2D>(bone2, bone1); }
		}
	}
}