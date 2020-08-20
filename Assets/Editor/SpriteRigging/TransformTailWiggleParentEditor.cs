using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SpriteRigging
{
	[CustomEditor(typeof(TransformTailWiggleParent))]
	public class TransformTailWiggleParentEditor : Editor
	{
		private TransformTailWiggleParent transformTailWiggleParent;

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

		private void GetElementListFromChildren ()
		{
			transformTailWiggleParent.elementList = GetAllChildren(transformTailWiggleParent.transform, true).ToArray();
		}

		private List<Transform> GetAllChildren (Transform target, bool recursive = true)
		{
			List<Transform> childList = new List<Transform>();
			for (int i = 0, iLimit = target.childCount; i < iLimit; i++)
			{
				Transform child = target.GetChild(i);

				childList.Add(child);
				if (recursive)
				{
					childList.AddRange(GetAllChildren(child, true));
				}
			}
			return childList;
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