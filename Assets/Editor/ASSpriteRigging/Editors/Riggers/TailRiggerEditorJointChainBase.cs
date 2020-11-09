using UnityEngine;
//using UnityEditor;

//using ASSPhysics.TailSystem;	//TailRootWiggle, ITailElement
using BoneRigging = ASSpriteRigging.BoneUtility.BoneRigging;

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
				BoneRigging.BoneConnectJoint<ConfigurableJoint>(
					bone: rootBone,
					targetRigidbody: inspector.targetAnchor,
					sample: inspector.defaultTailAnchorJoint
				);
			}
		}

		//rig an individual element of the transform chain
		protected override void RigTailBone (Transform bone, TInspector inspector)
		{
			BoneRigging.BoneSetTagAndLayer(bone, inspector.defaultTag, inspector.defaultLayer);
			BoneRigging.BoneSetupComponent<Rigidbody>(bone, inspector.defaultRigidbody);
			BoneRigging.BoneSetupComponent<SphereCollider>(bone, inspector.defaultCollider);
		}

		//rig a connection between two elements
		protected override void RigTailBonePairConnection (Transform bone, Transform nextBone, TInspector inspector)
		{
			BoneRigging.BoneConnectJoint<ConfigurableJoint>(bone, nextBone, inspector.defaultChainJoint);
		}
	//ENDOF abstract method implementation
	}
}