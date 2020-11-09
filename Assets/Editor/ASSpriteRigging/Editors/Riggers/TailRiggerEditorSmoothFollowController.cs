using UnityEngine;
using UnityEditor;

//using ASSPhysics.TailSystem;	//TailRootWiggle, ITailElement
using BoneRigging = ASSpriteRigging.BoneUtility.BoneRigging;

using TInspector = ASSpriteRigging.Riggers.TailRiggerInspectorSmoothFollowController;

using TElementController = ASSPhysics.TailSystem.TailElementJointSmoothFollow;

namespace ASSpriteRigging.Editors
{
//rigs a chain of bones with required components
	[CustomEditor(typeof(TInspector))]
	public class TailRiggerEditorSmoothFollowController
		: TailRiggerEditorJointChainBase<TInspector>
	{
	//overrides
		//rig an individual element of the transform chain
		protected override void RigTailBone (Transform bone, TInspector inspector)
		{
			base.RigTailBone(bone, inspector);
			//after rigging physics components create a chain element controller unless this is the last element in chain
			if (bone.childCount > 0)
			{
				BoneRigging.BoneSetupComponent<TElementController>(bone, inspector.defaultTailElementController);
			}
		}

		//rig a connection between two elements. also store the joint in the controller
		protected override void RigTailBonePairConnection (Transform bone, Transform nextBone, TInspector inspector)
		{
			TElementController elementController = bone.GetComponent<TElementController>();
			ConfigurableJoint connectionJoint = BoneRigging.BoneConnectJoint<ConfigurableJoint>(bone, nextBone, inspector.defaultChainJoint);
			if (elementController != null) { elementController.joint = connectionJoint; }
		}
	//ENDOF overrides
	}
}