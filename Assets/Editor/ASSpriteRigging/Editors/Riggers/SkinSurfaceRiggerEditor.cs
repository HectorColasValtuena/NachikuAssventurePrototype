using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ASSpriteRigging.Riggers; //SpriteSkinBaseRigger, SpriteSkinRigger
using ASSpriteRigging.BoneUtility;

namespace ASSpriteRigging.Editors
{
	[CustomEditor(typeof(SkinSurfaceRiggerInspector))]
	public class SkinSurfaceRiggerEditor : RiggerEditorBase<SkinSurfaceRiggerInspector>
	{
	//inherited abstract method implementation
		protected override void RigBones ()
		{
			Debug.Log("Rigging bone components for " + targetInspector.name);

			RigBoneMesh(
				boneList: targetInspector.spriteSkin.boneTransforms,
				anchorRigidbody: targetInspector.anchorRigidbody,
				triangles: targetInspector.sprite.GetIndices(),
				defaultRigidbody: targetInspector.defaultRigidbody,
				defaultAnchorJoint: targetInspector.defaultAnchorJoint,
				defaultMeshJoint: targetInspector.defaultMeshJoint,
				defaultCollider: targetInspector.defaultCollider,
				defaultTag: targetInspector.defaultTag,
				defaultLayer: targetInspector.defaultLayer
			);

			Debug.Log("Rigged bones of " + targetInspector.name);
		}
	//ENDOF inherited abstract method implementation

	//private methods
		private void RigBoneMesh <
			TAnchorJoint,
			TMeshJoint,
			TCollider
		> (
			Transform[] boneList,
			Rigidbody anchorRigidbody,
			NativeArray<ushort> triangles,
			Rigidbody defaultRigidbody,
			TAnchorJoint defaultAnchorJoint,
			TMeshJoint defaultMeshJoint,
			TCollider defaultCollider,
			string defaultTag = null,
			int defaultLayer = -1
		)
			where TAnchorJoint: Joint
			where TMeshJoint: Joint
			where TCollider: Collider
		{
			foreach (Transform bone in boneList)
			{
				RigBoneIndividualElements<
					TAnchorJoint,
					TCollider
				> (
					bone: bone,
					anchorRigidbody: anchorRigidbody,
					defaultRigidbody: defaultRigidbody,
					defaultAnchorJoint: defaultAnchorJoint,
					defaultCollider: defaultCollider,
					defaultTag: defaultTag,
					defaultLayer: defaultLayer
				);
			}

			Debug.Log ("Individual components rigged, deploying spring mesh");

			RigBoneSpringMesh<TMeshJoint>(boneList, triangles, defaultMeshJoint);

			Debug.Log ("Rigging bones finished");
		}

		//creates components that affect a single bone: rigidbodies and disconnected anchors/joints
		private void RigBoneIndividualElements <
			TAnchorJoint,
			TCollider
		> (
			Transform bone,
			Rigidbody anchorRigidbody,
			Rigidbody defaultRigidbody,
			TAnchorJoint defaultAnchorJoint,
			TCollider defaultCollider,
			string defaultTag = null,
			int defaultLayer = -1
		) 
			where TAnchorJoint: Joint
			where TCollider: Collider
		{
			//set the object tag and physics layer of the bone transform
			BoneRigging.BoneSetTagAndLayer(bone, defaultTag, defaultLayer);

			//give it a rigidbody and a colider
			BoneRigging.BoneSetupComponent<Rigidbody>(bone, defaultRigidbody);
			if (defaultCollider != null)
			{ BoneRigging.BoneSetupComponent<TCollider>(bone, defaultCollider);	}

			//create a joint anchoring the bone to target anchor rigidbody
			if (defaultAnchorJoint != null)
			{ BoneRigging.BoneConnectJoint<TAnchorJoint>(bone, anchorRigidbody, defaultAnchorJoint); }
		}

		//Generate springs between bones connected according to a triangle list
		//triangleList contains a multiple of 3 entries, and each 3 entries define a triangle
		private void RigBoneSpringMesh 
			<TJoint>
			(Transform[] boneList, NativeArray<ushort> triangleList, TJoint sample)
			where TJoint: Joint
		{
			if ((triangleList.Length % 3) != 0) { Debug.LogWarning("RigBoneSpringMesh() triangles.Length is not a multiple of 3"); }
			for (int i = 0, iLimit = triangleList.Length; i < iLimit; i += 3)
			{
				//for every 3 vertex entries, process them as a triangle
				BoneGenerateSpringPolygon (boneList, triangleList.GetSubArray(i, 3), sample);
			}
		}

		//Generates the springs for a single polygon
		private void BoneGenerateSpringPolygon
			<TJoint>
			(Transform[] boneList, NativeArray<ushort> polygon, TJoint sample)
			where TJoint: Joint
		{
			//first element will connect to the last enclosing the polygon
			int previousBone = polygon.Length - 1;

			for (int i = 0, iLimit = polygon.Length; i < iLimit; i++)
			{
				//connect every bone to the previous bone in the polygon
				BoneRigging.InterconnectBonePair<TJoint>(
					bone1: boneList[polygon[i]],
					bone2: boneList[polygon[previousBone]],
					sample: sample,
					mutual: false
				);
				previousBone = i;
			}
		}
	}
}