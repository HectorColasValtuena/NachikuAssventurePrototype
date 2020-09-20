using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ASSpriteRigging;	//TailRootRigger
using ASSpriteRigging.BoneUtility;
//using ASSpriteRigging.BoneRigging;
using ASSPhysics.TailSystem;

namespace ASSpriteRigging.Editors
{
	/*
	[CustomEditor(typeof(TailRootWiggleTransform))]
	public class TailRootWiggleTransformEditor : TailWiggleParentEditor {}
	*/
	[CustomEditor(typeof(TailRootRigger))]
	public class TailRootRiggerEditor : Editor
	{
		//private TailRootWiggle tailRootWiggle;
		private TailRootRigger tailRootRigger;

	//Setup GUI layout
		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI();

			tailRootRigger = (TailRootRigger) target;

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
				TailRigging.RigTail(tailRootWiggle);
			}
		}
	//ENDOF Setup GUI layout

		private void GetElementListFromChildren ()
		{
			Debug.LogWarning("Tail editor method GetElementListFromChildren still uses BoneHierarchy.GetChildren Method. Danger of breaking upon updating GetChildren");
			tailRootWiggle.elementList = BoneHierarchy.GetChildren(tailRootWiggle.transform, recursive: true, includeIgnored: true).ToArray();
		}

		
	}
}