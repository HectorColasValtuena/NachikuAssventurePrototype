using System.Reflection;

using UnityEngine;
using ASSPhysics.TailSystem;	//TailRootWiggle, ITailElement

using ASSpriteRigging.Riggers;

namespace ASSpriteRigging.BoneUtility
{
	public static class TailRigging
	{
		private static readonly BindingFlags privateMethodBindings =
			BindingFlags.Static |
			BindingFlags.NonPublic;


	//Tail generation
		//extracts all the data from rigger object and calls a properly parametrized RigTailBoneRecursive
		public static void RigTail (TailRootRigger rigger)
		{	
			//generate a type footprint from the component types of target rigger
			System.Type[] typeList =
			{
				rigger.defaultTailElement.GetType(),
				rigger.defaultCollider.GetType(),
				rigger.defaultChainJoint.GetType()
			};
		
			//compose parameter list for invokation
			System.Object[] parameters = {
				rigger.transform,				//Transform bone
				rigger.defaultTailElement,		//TTailElement defaultTailElement,
				rigger.defaultRigidbody,		//Rigidbody2D defaultRigidbody
				rigger.defaultCollider,			//CircleCollider2D defaultCollider
				rigger.defaultChainJoint,		//FixedJoint2D defaultChainJoint
				rigger.defaultTag,				//string defaultTag
				rigger.defaultLayer				//int defaultLayer
			};

			//invoke the call having properly set-up types and parameters
			CallMethodWithTypesAndParameters (
				type: typeof (TailRigging),				//System.Type type
				context: null,						//System.Object context,
				methodName:	"RigTailBoneElementRecursive",		//string methodName,
				bindingFlags: privateMethodBindings,	//BindingFlags bindingFlags, 
				typeList: typeList,						//System.Type[] typeList,
				parameters:	parameters					//System.Object[] parameters
			);
		}


		//Recursively populate every transform with adequate controller and components
		private	static void RigTailBoneElementRecursive <
			TTailElement,
			TCollider,
			TChainJoint
		> (
			Transform bone,
			TTailElement defaultTailElement,
			Rigidbody2D defaultRigidbody,
			TCollider defaultCollider,
			TChainJoint defaultChainJoint,
			string defaultTag = null,
			int defaultLayer = -1
		)
			where TTailElement: UnityEngine.Component, ITailElement
			where TCollider: Collider2D
			where TChainJoint: Joint2D
		{
			Debug.Log("bone: "); Debug.LogWarning(bone);
			BoneRigging.BoneSetTagAndLayer(bone, defaultTag, defaultLayer);
			BoneRigging.BoneSetupComponent<Rigidbody2D>(bone, defaultRigidbody);
			BoneRigging.BoneSetupComponent<TCollider>(bone, defaultCollider);

			//last element of the chain doesn't need controller or joint. abort if no more descendants
			if (bone.childCount < 1) return;

			//create the element controller
			TTailElement newElement = BoneRigging.BoneSetupComponent<TTailElement>(bone, defaultTailElement);

			//try to find next element in chain pre-existing, otherwise manually create it
			Transform next = (newElement.childElement as Component)?.transform;
			if (next == null) { next = bone.GetChild(0); }

			//connect a joint to the next element corresponding
			//BoneRigging.BoneSetupComponent<Rigidbody2D>(next, defaultRigidbody);

			//now setup next element in chain
			//we set up next element before the joint so the rigidbody will be created first
			RigTailBoneElementRecursive<
				TTailElement,
				TCollider,
				TChainJoint
			> (
				next,	//Transform bone,
				defaultTailElement,	//TTailElement defaultTailElement,
				defaultRigidbody,	//Rigidbody2D defaultRigidbody,
				defaultCollider,	//TCollider defaultCollider,
				defaultChainJoint,	//TChainJoint defaultChainJoint,
				defaultTag,			//string defaultTag = null,
				defaultLayer		//int defaultLayer = -1
			);

			//finally connect the joint to the next element
			BoneRigging.BoneConnectJoint<TChainJoint>(bone, next, defaultChainJoint);

		}

		/////////////////////////////////////////////////////////////////////////////////////////////////////
		//[TO-DO] move this elsewhere
		//Dynamically compose a generic method call and invoke with target parameters
		private static void CallMethodWithTypesAndParameters (
			System.Type type,
			System.Object context,
			string methodName,
			BindingFlags bindingFlags, 
			System.Type[] typeList,
			System.Object[] parameters
		) {
			//context
			//	.GetType()
			type
				.GetMethod(methodName, bindingFlags)
				.MakeGenericMethod(typeList)
				.Invoke(context, parameters);	//(context, parameters);
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