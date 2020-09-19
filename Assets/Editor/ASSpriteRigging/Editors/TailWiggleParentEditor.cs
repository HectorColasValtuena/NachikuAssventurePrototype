using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ASSpriteRigging.BoneUtility;
//using ASSpriteRigging.BoneRigging;
using ASSPhysics.TailSystem;

namespace ASSpriteRigging.Editors
{
	/*
	[CustomEditor(typeof(TailRootWiggleTransform))]
	public class TailRootWiggleTransformEditor : TailWiggleParentEditor {}
	*/
	[CustomEditor(typeof(TailRootWiggle))]
	public class TailRootWiggleEditor : Editor
	{
		private TailRootWiggle tailWiggleParent;

	//Setup GUI layout
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI();

			tailWiggleParent = (TailRootWiggle) target;

			DoGetElementListFromChildrenButton();
			DoAddComponentToElementListButton();
		}

		public void DoGetElementListFromChildrenButton ()
		{
			if (GUILayout.Button("Get element list", GUILayout.MaxWidth(200f))) { GetElementListFromChildren(); }
		}
		public void DoAddComponentToElementListButton ()
		{
			if (GUILayout.Button("Add controller to elements", GUILayout.MaxWidth(200f)))
			{
				TailRigging.RigTail(tailWiggleParent);
			}
		}
	//ENDOF Setup GUI layout

		private void GetElementListFromChildren ()
		{
			Debug.LogWarning("Tail editor method GetElementListFromChildren still uses BoneHierarchy.GetChildren Method. Danger of breaking upon updating GetChildren");
			tailWiggleParent.elementList = BoneHierarchy.GetChildren(tailWiggleParent.transform, recursive: true, includeIgnored: true).ToArray();
		}

		
	}
}