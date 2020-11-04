using System.Reflection;

using UnityEngine;

using ASSPhysics.TailSystem;	//TailRootWiggle, ITailElement
using ASSpriteRigging.Riggers;	//TailRigger

using static ASSistant.ReflectionAssistant;


namespace ASSpriteRigging.BoneUtility
{
	public static class TailRigging
	{
		private static readonly BindingFlags privateMethodBindings =
			BindingFlags.Static |
			BindingFlags.NonPublic;


	//Tail generation
		//extracts all the data from rigger object and calls a properly parametrized RigTailBoneRecursive
		public static void RigTail (TailRootRiggerInspector rigger)
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
			Rigidbody defaultRigidbody,
			TCollider defaultCollider,
			TChainJoint defaultChainJoint,
			string defaultTag = null,
			int defaultLayer = -1
		)
			where TTailElement: UnityEngine.Component, ASSPhysics.PulseSystem.PulsePropagators.IPulsePropagator
			where TCollider: Collider
			where TChainJoint: Joint
		{
			Debug.Log("bone: "); Debug.LogWarning(bone);
			BoneRigging.BoneSetTagAndLayer(bone, defaultTag, defaultLayer);
			BoneRigging.BoneSetupComponent<Rigidbody>(bone, defaultRigidbody);
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
				bone: next,	//Transform bone,
				defaultTailElement: defaultTailElement,	//TTailElement defaultTailElement,
				defaultRigidbody: defaultRigidbody,	//Rigidbody2D defaultRigidbody,
				defaultCollider: defaultCollider,	//TCollider defaultCollider,
				defaultChainJoint: defaultChainJoint,	//TChainJoint defaultChainJoint,
				defaultTag: defaultTag,			//string defaultTag = null,
				defaultLayer: defaultLayer		//int defaultLayer = -1
			);

			//finally connect the joint to the next element
			BoneRigging.BoneConnectJoint<TChainJoint>(bone, next, defaultChainJoint);

		}
	//ENDOF Tail Generation
	}
}