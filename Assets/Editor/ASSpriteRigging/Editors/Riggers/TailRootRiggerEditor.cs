using System.Reflection;

using UnityEngine;
using UnityEditor;

using ASSPhysics.TailSystem;	//TailRootWiggle, ITailElement
using ASSpriteRigging.Riggers;	//TailRigger
using BoneRigging = ASSpriteRigging.BoneUtility.BoneRigging;

using static ASSistant.ReflectionAssistant;

namespace ASSpriteRigging.Editors
{
//rigs a chain of bones with required components
	//[CustomEditor(typeof(TailRootRiggerInspector))]
	public class TailRootRiggerEditor<TInspector> : RiggerEditorBase<TInspector>
		where TInspector : TailRootRiggerInspector
	{
	//constant definitions
		private static readonly BindingFlags privateMethodBindings =
			BindingFlags.Instance |
			BindingFlags.NonPublic;
	//ENDOF constant definitions

	//abstract method implementation
		protected override void RigBones ()
		{
			RigTail(targetInspector);
			Debug.Log("Rigged bones of " + targetInspector.name);
		}
	//ENDOF abstract method implementation

	//private methods
		//extracts all the data from rigger object and calls a properly parametrized RigTailBoneRecursive
		private void RigTail (TailRootRiggerInspector inspector)
		{	
			//generate a type footprint from the component types of target rigger
			System.Type[] typeList =
			{
				inspector.defaultTailElement.GetType(),
				inspector.defaultCollider.GetType(),
				inspector.defaultChainJoint.GetType()
			};
		
			//compose parameter list for invokation
			System.Object[] parameters = {
				inspector.transform,				//Transform bone
				inspector.defaultTailElement,		//TTailElement defaultTailElement,
				inspector.defaultRigidbody,		//Rigidbody2D defaultRigidbody
				inspector.defaultCollider,			//CircleCollider2D defaultCollider
				inspector.defaultChainJoint,		//FixedJoint2D defaultChainJoint
				inspector.defaultTag,				//string defaultTag
				inspector.defaultLayer				//int defaultLayer
			};

			//invoke the call having properly set-up types and parameters
			CallMethodWithTypesAndParameters (
				type: typeof (TailRootRiggerEditor<TInspector>),	//System.Type type
				context: this,						//System.Object context,
				methodName:	"RigTailBoneElementRecursive",		//string methodName,
				bindingFlags: privateMethodBindings,	//BindingFlags bindingFlags, 
				typeList: typeList,						//System.Type[] typeList,
				parameters:	parameters					//System.Object[] parameters
			);
		}

		//Recursively populate every transform with adequate controller and components
		private	void RigTailBoneElementRecursive <
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
			newElement.SetParent(bone.parent.GetComponent<TTailElement>());

			//loop over this element's transform children, recursively rigging each of them
			for (int i = 0, iLimit = bone.childCount; i < iLimit; i++)
			{
				Transform next = bone.GetChild(i);
				//setup next element in chain
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

				//finally connect the joint to the next element so child rigidbody now exists
				BoneRigging.BoneConnectJoint<TChainJoint>(bone, next, defaultChainJoint);
			}
		}
	//ENDOF private methods
	}
}