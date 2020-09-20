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

			BoneRigging.RigBoneSpringMesh (boneList, triangles, defaultSpring);

			Debug.Log("Rigging bones finished");
		}

		//creates components that affect a single bone: rigidbodies and disconnected anchors/joints
		private static void RigBoneIndividualElements (Transform bone, Rigidbody2D defaultRigidbody, SpringJoint2D defaultAnchor, CircleCollider2D defaultCollider, string defaultTag = null, int defaultLayer = -1)
		{
			BoneRigging.BoneCreateRigidbody(bone, defaultRigidbody);
			BoneRigging.BoneCreateAnchor(bone, defaultAnchor);
			BoneRigging.BoneCreateCollider(bone, defaultCollider);
			BoneRigging.BoneSetTagAndLayer(bone, defaultTag, defaultLayer);
		}
	}
}