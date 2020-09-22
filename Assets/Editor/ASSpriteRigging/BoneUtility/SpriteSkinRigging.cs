using UnityEngine;
using Unity.Collections; //NativeArray<T>
using static UnityEngine.U2D.SpriteDataAccessExtensions; //Sprite.GetIndices() extension method

using ASSpriteRigging.Riggers; //SpriteSkinRigger

namespace ASSpriteRigging.BoneUtility
{
	public static class SpriteSkinRigging
	{
		public static void RigBones (SpriteSkinRigger target)
		{
			Debug.Log("Rigging bone components for " + target.gameObject.name);
			RigBones(
				boneList: target.spriteSkin.boneTransforms,
				triangles: target.sprite.GetIndices(),
				defaultRigidbody: target.defaultRigidbody,
				defaultAnchor: target.defaultAnchor,
				defaultSpring: target.defaultSpring,
				defaultCollider: target.defaultCollider,
				defaultTag: target.defaultTag,
				defaultLayer: target.defaultLayer
			);
		}
		public static void RigBones (Transform[] boneList, NativeArray<ushort> triangles, Rigidbody2D defaultRigidbody, SpringJoint2D defaultAnchor, SpringJoint2D defaultSpring, CircleCollider2D defaultCollider, string defaultTag = null, int defaultLayer = -1)
		{
			foreach (Transform bone in boneList)
			{
				RigBoneIndividualElements (
					bone: bone,
					defaultRigidbody: defaultRigidbody,
					defaultAnchor: defaultAnchor,
					defaultCollider: defaultCollider,
					defaultTag: defaultTag,
					defaultLayer: defaultLayer
				);
			}

			Debug.Log ("Individual components rigged, deploying spring mesh");

			RigBoneSpringMesh (boneList, triangles, defaultSpring);

			Debug.Log ("Rigging bones finished");
		}

		//creates components that affect a single bone: rigidbodies and disconnected anchors/joints
		private static void RigBoneIndividualElements (Transform bone, Rigidbody2D defaultRigidbody, SpringJoint2D defaultAnchor, CircleCollider2D defaultCollider, string defaultTag = null, int defaultLayer = -1)
		{
			//set the object tag and physics layer of the bone transform
			BoneRigging.BoneSetTagAndLayer(bone, defaultTag, defaultLayer);

				//give it a rigidbody and a colider
			BoneRigging.BoneSetupComponent<Rigidbody2D>(bone, defaultRigidbody);
			BoneRigging.BoneSetupComponent<CircleCollider2D>(bone, defaultCollider);

			//create a connection towards its parent in the manner of an anchoring
			BoneRigging.BoneConnectJoint<SpringJoint2D>(bone, bone.parent, defaultAnchor);
		}

		//Generate springs between bones connected according to a triangle list
		//triangleList contains a multiple of 3 entries, and each 3 entries define a triangle
		public static void RigBoneSpringMesh (Transform[] boneList, NativeArray<ushort> triangleList, SpringJoint2D sample)
		{
			if ((triangleList.Length % 3) != 0) { Debug.LogWarning("RigBoneSpringMesh() triangles.Length is not a multiple of 3"); }
			for (int i = 0, iLimit = triangleList.Length; i < iLimit; i += 3)
			{
				//for every 3 vertex entries, process them as a triangle
				BoneGenerateSpringPolygon (boneList, triangleList.GetSubArray(i, 3), sample);
			}
		}

		//Generates the springs for a single polygon
		private static void BoneGenerateSpringPolygon (Transform[] boneList, NativeArray<ushort> polygon, SpringJoint2D sample)
		{
			//first element will connect to the last enclosing the polygon
			int previousBone = polygon.Length - 1;

			for (int i = 0, iLimit = polygon.Length; i < iLimit; i++)
			{
				//connect every bone to the previous bone in the polygon
				BoneRigging.InterconnectBonePair<SpringJoint2D>(
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