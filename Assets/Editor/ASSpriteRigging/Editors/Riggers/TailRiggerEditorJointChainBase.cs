using UnityEngine;
//using UnityEditor;

//using ASSPhysics.TailSystem;	//TailRootWiggle, ITailElement
using BoneRigging = ASSpriteRigging.BoneUtility.BoneRigging;
using ASSistant.ComponentConfiguration.JointConfiguration;

namespace ASSpriteRigging.Editors
{
//rigs a chain of bones with required components
	public abstract class TailRiggerEditorJointChainBase<TInspector>
		: TailRiggerEditorBase<TInspector>
		where TInspector: ASSpriteRigging.Riggers.TailRiggerInspectorJointChain
	{
	//abstract method implementation
		//rig the base/root of the transform chain
		protected override void RigTailRoot (Transform rootBone, TInspector inspector)
		{
			if (inspector.defaultTailAnchorJoint != null)
			{
				foreach (Rigidbody tailAnchor in inspector.tailAnchorList)
				{
					BoneRigging.BoneConnectJoint<ConfigurableJoint>(
						bone: rootBone,
						targetRigidbody: tailAnchor,
						sample: inspector.defaultTailAnchorJoint
					);
				}
			}
		}

		//rig an individual element of the transform chain
		protected override void RigTailBone (Transform bone, TInspector inspector)
		{
			BoneRigging.BoneSetTagAndLayer(bone, inspector.defaultTag, inspector.defaultLayer);
			BoneRigging.BoneSetupComponent<Rigidbody>(bone, inspector.defaultRigidbody);
			if (inspector.defaultCollider != null)
			{ BoneRigging.BoneSetupComponent<SphereCollider>(bone, inspector.defaultCollider); }
		}

		//rig a connection between two elements
		protected override ConfigurableJoint RigTailBonePairConnection (Transform bone, Transform nextBone, TInspector inspector)
		{
			Debug.Log("TailRiggerEditorJointChainBase.RigTailBonePairConnection();");
			//create a joint and initialize its anchors as a chain setup then return the joint
			return BoneRigging
				.BoneConnectJoint<ConfigurableJoint>(bone, nextBone, inspector.defaultChainJoint)
				.EMSetChainAnchor();
		}
	//ENDOF abstract method implementation
	}
}