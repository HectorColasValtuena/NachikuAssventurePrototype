using UnityEngine;

using BoneRigging = ASSpriteRigging.BoneUtility.BoneRigging;
using ASSistant.ComponentConfiguration.JointConfiguration; //ConfigurableJoint extension methods

using IJointChainRiggerInspector = ASSpriteRigging.Inspectors.IJointChainRiggerInspector;

namespace ASSpriteRigging.Editors
{
//rigs a chain of bones with required components
	public abstract class TailRiggerEditorJointChainBase<TInspector>
	:
		TailRiggerEditorBase<TInspector>
		where TInspector : UnityEngine.Object, IJointChainRiggerInspector
	{
	//abstract method implementation
		//rig the base/root of the transform chain
		protected override void RigTailRoot (Transform rootBone, TInspector inspector)
		{
			if (inspector.defaultRootAnchorJoint != null)
			{
				foreach (Rigidbody rootAnchor in inspector.rootAnchorList)
				{
					BoneRigging.BoneConnectJoint<ConfigurableJoint>(
						bone: rootBone,
						targetRigidbody: rootAnchor,
						sample: inspector.defaultRootAnchorJoint
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