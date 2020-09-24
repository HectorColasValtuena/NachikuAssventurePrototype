using System.Reflection;

using UnityEngine;
using ASSPhysics.TailSystem;	//TailRootWiggle

using ASSpriteRigging.Riggers;

namespace ASSpriteRigging.BoneUtility
{
	public static class TailRigging
	{
		private static readonly BindingFlags privateMethodBindings =
			BindingFlags.Static |
			BindingFlags.NonPublic;

	//Tail generation
		public static void RigTail (TailRootRigger rigger)
		{	
			//generate a type footprint from the component types of target rigger
			System.Type[] typeList =
			{
				rigger.defaultCollider.GetType(),
				rigger.defaultChainJoint.GetType()
			};
			//create a generic method typed according to desired components
			MethodInfo riggingMethodCall = 
				rigger
				.GetType()
				.GetMethod("RigTailBoneRecursive", privateMethodBindings)
				.MakeGenericMethod(typeList)
			;

			//compose parameter list for invokation
			System.Object[] parameters = {
				rigger.transform,				//Transform rootBone
				rigger.defaultRigidbody,		//Rigidbody2D defaultRigidbody
				rigger.defaultCollider,			//CircleCollider2D defaultCollider
				rigger.defaultChainJoint,		//FixedJoint2D defaultChainJoint
				rigger.defaultTag,				//string defaultTag
				rigger.defaultLayer				//int defaultLayer
			};
			//invoke the call having properly set-up types and parameters
			riggingMethodCall.Invoke(rigger, parameters);	
		}

		private	static void RigTailBoneRecursive <
			TCollider,
			TChainJoint
		> (
			Transform rootBone,
			Rigidbody2D defaultRigidbody,
			TCollider defaultCollider,
			TChainJoint defaultChainJoint,
			string defaultTag = null,
			int defaultLayer = -1
		)
			where TCollider: Collider2D
			where TChainJoint: Joint2D
		{
			
		}
		/*public static void RigTail (Transform rootBone, Rigidbody2D defaultRigidbody, CircleCollider2D defaultCollider, FixedJoint2D defaultChainJoint, string defaultTag = null, int defaultLayer = -1)
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
			*/

		/*
		private static void RigTailBone (Transform bone, Transform nextBone, Rigidbody2D defaultRigidbody, CircleCollider2D defaultCollider, FixedJoint2D defaultChainJoint, string defaultTag = null, int defaultLayer = -1)
		{
			BoneRigging.AddComponentToTransform<TailWiggleElementTransform>(bone); //BoneSetupComponent
			BoneRigging.BoneCreateRigidbody(bone, defaultRigidbody);
			BoneRigging.BoneCreateCollider(bone, defaultCollider);
			BoneRigging.BoneSetTagAndLayer(bone, defaultTag, defaultLayer);
		}
		*/

	//ENDOF Tail Generation
	}
}