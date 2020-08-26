using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
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

			sprite = ((SpriteExperiments) target).gameObject.GetComponent<Sprite>();
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

			for (int i = 0, iLimit = sprite.GetVertexCount(); i < iLimit; i++)
			{
				Debug.Log("========\nVertex #" + i);

			}
		}
	}
}