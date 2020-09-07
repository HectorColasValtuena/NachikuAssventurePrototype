using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ASSpriteRigging.BoneUtility;
using ASSpriteRigging.TailSystem;

namespace ASSpriteRigging.Editors
{
	[CustomEditor(typeof(TransformTailWiggleParent))]
	public class TransformTailWiggleParentEditor : Editor
	{
		private TransformTailWiggleParent transformTailWiggleParent;

	//Setup GUI layout
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI();

			transformTailWiggleParent = (TransformTailWiggleParent) target;

			DoGetElementListFromChildrenButton();
			DoAddComponentToElementListButton();
		}

		public void DoGetElementListFromChildrenButton ()
		{
			if (GUILayout.Button("Get element list", GUILayout.MaxWidth(200f))) { GetElementListFromChildren(); }
		}
		public void DoAddComponentToElementListButton()
		{
			if (GUILayout.Button("Add controller to elements", GUILayout.MaxWidth(200f))) { AddComponentToTransformList<TransformTailWiggleElement>(transformTailWiggleParent.elementList); }	
		}
	//ENDOF Setup GUI layout

		private void GetElementListFromChildren ()
		{
			transformTailWiggleParent.elementList = BoneHierarchy.GetChildren(transformTailWiggleParent.transform, recursive: true, includeIgnored: true).ToArray();
		}

		private void AddComponentToTransformList <T> (Transform[] transformList) where T : MonoBehaviour
		{
			foreach (Transform element in transformList)
			{
				if (element.GetComponent<T>() == null)
				{
					ObjectFactory.AddComponent<T>(element.gameObject);
				}
			}
		}
	}
}