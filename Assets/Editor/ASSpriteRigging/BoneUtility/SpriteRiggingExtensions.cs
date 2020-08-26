using System.Collections;
using System.Collections.Generic;
using Unity.Collections;	//NativeSlice
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Rendering;

namespace ASSpriteRigging.BoneUtility
{
	public static class SpriteRiggingExtensions
	{
		//sets up the bone list and weights from vertex list
		public static void BonesFromVertexList (this Sprite _this)
		{
		//validate if vertex information available
			if (!_this.HasVertexAttribute(VertexAttribute.Position))
			{
				Debug.LogWarning("Sprite has no vertex position attribute. Aborting.");
				return;
			}

		//cache vertex position list and create corresponding bone and weights
			NativeSlice<Vector3> vertexList = _this.GetVertexAttribute<Vector3>(VertexAttribute.Position);
			for (int i = 0, iLimit = vertexList.Length; i < iLimit; i++)
			{
				//===============================================================================================
				//[TO-DO]
				//===============================================================================================
			}
		}
	}
}