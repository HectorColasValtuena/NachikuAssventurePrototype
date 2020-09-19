using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ASSpriteRigging.BoneUtility;
using ASSPhysics.TailSystem;

namespace ASSpriteRigging.Editors
{
	[CustomEditor(typeof(TailWiggleParentBase))]
	public class TailWiggleParentEditor : Editor
	{
		private TailWiggleParentBase tailWiggleParent;

	//Setup GUI layout
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI();

			tailWiggleParent = (TailWiggleParentBase) target;

			DoGetElementListFromChildrenButton();
			DoAddComponentToElementListButton();
		}

		public void DoGetElementListFromChildrenButton ()
		{
			if (GUILayout.Button("Get element list", GUILayout.MaxWidth(200f))) { GetElementListFromChildren(); }
		}
		public void DoAddComponentToElementListButton()
		{
			if (GUILayout.Button("Add controller to elements", GUILayout.MaxWidth(200f)))
			{
				AddComponentToTransformList<TailWiggleElementTransform>(tailWiggleParent.elementList);
				//this is where element type 
				/////////////////////////////////////////////////////////////////////////////////////////
			}
		}
	//ENDOF Setup GUI layout

		private void GetElementListFromChildren ()
		{
			Debug.LogWarning("Tail editor method GetElementListFromChildren still uses BoneHierarchy.GetChildren Method. Danger of breaking upon updating GetChildren");
			tailWiggleParent.elementList = BoneHierarchy.GetChildren(tailWiggleParent.transform, recursive: true, includeIgnored: true).ToArray();
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