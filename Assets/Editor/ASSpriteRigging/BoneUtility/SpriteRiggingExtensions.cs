using System.Collections;
using System.Collections.Generic;
using Unity.Collections;		//NativeSlice, NativeArray
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Rendering;	//VertexAttribute
using UnityEditor; 				//Undo, AssetDatabase

namespace ASSpriteRigging.BoneUtility
{
	public static class SpriteRiggingExtensions
	{
		//sets up the bone list and weights from vertex list
		public static void BonesFromVertexList (this Sprite _this, string boneBaseName)
		{
			Debug.LogWarning("Generating sprite bones");
		//validate if vertex information available
			if (!_this.HasVertexAttribute(VertexAttribute.Position))
			{
				Debug.LogWarning("Sprite has no vertex position attribute. Aborting.");
				return;
			}

		//iterate through the vertex list creating corresponding bones and weights
			//cache vertex position list
			NativeSlice<Vector3> vertexList = _this.GetVertexAttribute<Vector3>(VertexAttribute.Position);
			//create lists to hold created bones and weights
			SpriteBone[] boneList = new SpriteBone[vertexList.Length];
			BoneWeight[] weightList = new BoneWeight[vertexList.Length];

			//loop over vertex list
			for (int i = 0, iLimit = vertexList.Length; i < iLimit; i++)
			{
				boneList[i] = CreateBoneForVertex(i, boneBaseName, vertexList[i]);
				weightList[i] = CreateSimpleVertexWeight(i);
			}
			_this.name += "[AUTORIG]";
		//apply the lists of bones and weights to the sprite, storing an undo snapshot to allow ctrl+z
			Undo.RecordObject(_this, "Auto-generated bones for sprite \"" + _this.name + "\"");
			_this.SetBones(boneList);
			_this.SetVertexAttribute<BoneWeight>(VertexAttribute.BlendWeight, new NativeArray<BoneWeight>(weightList, Allocator.Temp));
			Debug.Log ("Added " + _this.GetBones().Length + " bones to sprite " + _this.name);


			StoreAsset(_this);
			//AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(_this));
			//AssetDatabase.CreateAsset(_this, "Assets/Tmp/GeneratedAss.asset");
		}

		private static void StoreAsset (Sprite target)
		{
			AssetDatabase.StartAssetEditing();
			//AssetDatabase.RemoveObjectFromAsset(target);
			//AssetDatabase.CreateAsset(target, "Assets/Tmp/GeneratedAss.asset");
			target.ApplyModifiedProperties();
			AssetDatabase.StopAssetEditing();
		}

		//creates a bone object
		private static SpriteBone CreateBoneForVertex (
			int index,
			string baseName,
			Vector3 targetPosition,
			Quaternion targetRotation = new Quaternion(),
			int targetParent = 0,
			float targetLength = 0f
		) {
			SpriteBone newBone = new SpriteBone();
			newBone.name = baseName + index;
			newBone.position = targetPosition;
			newBone.rotation = targetRotation;
			newBone.parentId = targetParent;
			newBone.length = targetLength;
			return newBone;
		} 

		//creates a simple skinning weight linking one vertex to one bone
		private static BoneWeight CreateSimpleVertexWeight (int targetBone)
		{
			BoneWeight newWeight = new BoneWeight();
			newWeight.boneIndex0 = targetBone;
			newWeight.weight0 = 1f;
			return newWeight;
		}
		
	}
}