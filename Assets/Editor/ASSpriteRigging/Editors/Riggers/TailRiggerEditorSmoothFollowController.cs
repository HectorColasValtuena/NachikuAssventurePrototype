using UnityEngine;
using UnityEditor;

using BoneRigging = ASSpriteRigging.BoneUtility.BoneRigging;

using TInspector = ASSpriteRigging.Inspectors.TailRiggerInspectorSmoothFollowController;

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
		protected override ConfigurableJoint RigTailBonePairConnection (Transform bone, Transform nextBone, TInspector inspector)
		{
			ConfigurableJoint connectionJoint = base.RigTailBonePairConnection(bone, nextBone, inspector);
			TElementController elementController = bone.GetComponent<TElementController>();
			if (elementController != null) { elementController.joint = connectionJoint; }
			return connectionJoint;
		}
	//ENDOF overrides
	}
}