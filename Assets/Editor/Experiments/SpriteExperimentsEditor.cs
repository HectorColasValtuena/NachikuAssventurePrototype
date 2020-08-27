using System.Collections;
using System.Collections.Generic;
using Unity.Collections;	//NativeSlice
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Rendering;	//VertexAttribute
using UnityEditor;


namespace ASSperiments
{
	[CustomEditor(typeof(SpriteExperiments))]
	public class SpriteExperimentsEditor : Editor
	{
		Sprite sprite;

		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI();

			sprite = ((SpriteExperiments) target).gameObject.GetComponent<SpriteRenderer>().sprite;

			DoTestBonesButton();
		}

		private void DoTestBonesButton ()
		{
			if (GUILayout.Button("Test bones"))
			{ TestBones(sprite); }
		}

		private void TestBones(Sprite sprite)
		{
			Debug.Log("TestBones");

			//=============================================================================================================
			NativeSlice<Vector3> positionList = sprite.GetVertexAttribute<Vector3>(VertexAttribute.Position);
			//Debug.Log(sprite.GetVertexAttribute(VertexAttribute.BlendIndices).GetType());
			//NativeSlice<BoneWeight> blendIndicesList = sprite.GetVertexAttribute<BoneWeight>(VertexAttribute.BlendIndices);
			NativeSlice<BoneWeight> blendWeightList = sprite.GetVertexAttribute<BoneWeight>(VertexAttribute.BlendWeight);

			for (int i = 0, iLimit = sprite.GetVertexCount(); i < iLimit; i++)
			{
				Debug.Log("========\nVertex #" + i);
				Debug.Log(positionList[i]);
				//Debug.Log(blendIndicesList[i]);
				Debug.Log(blendWeightList[i]);
				Debug.Log(" > " + blendWeightList[i].boneIndex0 + ": " + blendWeightList[i].weight0);
				Debug.Log(" > " + blendWeightList[i].boneIndex1 + ": " + blendWeightList[i].weight1);
				Debug.Log(" > " + blendWeightList[i].boneIndex2 + ": " + blendWeightList[i].weight2);
				Debug.Log(" > " + blendWeightList[i].boneIndex3 + ": " + blendWeightList[i].weight3);
			}
		}
	}
}