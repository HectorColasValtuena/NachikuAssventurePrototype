using UnityEngine;
using ASSPhysics.TailSystem;	//TailRootWiggle

namespace ASSpriteRigging.BoneUtility
{
	public static class TailRigging
	{
	//Tail generation
		public static void RigTail (TailRootWiggle tailRoot)
		{
			Debug.LogWarning("You dindn't implement me yet you bastard hitler hitler hitler hitler hitler");
			//RigTailBoneList();
		}
		public static void RigTail (Transform [] boneList, Rigidbody2D defaultRigidbody, CircleCollider2D defaultCollider, FixedJoint2D defaultChainJoint, string defaultTag = null, int defaultLayer = -1)
		{

			//rig each bone except the last
			for (int i = 0, iLimit = boneList.Length - 1; i < iLimit; i++)
			{
				RigTailBone(
					bone: boneList[i],
					nextBone: boneList[i+1],
					defaultRigidbody: defaultRigidbody,
					defaultCollider: defaultCollider,
					defaultChainJoint: defaultChainJoint,
					defaultTag: defaultTag,
					defaultLayer: defaultLayer
				);
			}
		} 

		private static void RigTailBone (Transform bone, Transform nextBone, Rigidbody2D defaultRigidbody, CircleCollider2D defaultCollider, FixedJoint2D defaultChainJoint, string defaultTag = null, int defaultLayer = -1)
		{
			BoneRigging.AddComponentToTransform<TailWiggleElementTransform>(bone);
			BoneRigging.BoneCreateRigidbody(bone, defaultRigidbody);
			BoneRigging.BoneCreateCollider(bone, defaultCollider);
			BoneRigging.BoneSetTagAndLayer(bone, defaultTag, defaultLayer);
		}


	//ENDOF Tail Generation
	}
}