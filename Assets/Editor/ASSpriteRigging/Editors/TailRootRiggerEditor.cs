using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ASSpriteRigging.Riggers; //SpriteSkinBaseRigger, TailRootRigger
using ASSpriteRigging.BoneUtility;

using ASSPhysics.TailSystem; //TailRootWiggle

namespace ASSpriteRigging.Editors
{
	/*
	[CustomEditor(typeof(TailRootWiggleTransform))]
	public class TailRootWiggleTransformEditor : TailWiggleParentEditor {}
	*/
	[CustomEditor(typeof(TailRootRigger))]
	public class TailRootRiggerEditor : RiggerEditorBase
	{
		protected override bool isArmed { get { return tailRootRigger.armed; } set { tailRootRigger.armed = value; }}
		private TailRootRigger tailRootRigger;

	//Setup GUI layout
		protected override void InspectorInitialization ()
		{
			tailRootRigger = (TailRootRigger) target;
		}

		protected override void FullSetup ()
		{
			Debug.Log("Initiating full setup of " + target.name);
			BoneHierarchy.CreateBoneHierarchy(spriteSkinRigger);
			RigBones();
			Debug.Log("Full setup finished");
		}

		protected override void RigBones ()
		{
			SpriteSkinRigging.RigBones(spriteSkinRigger);
			Debug.Log("Rigged bones of " + target.name);
		}		
/*
		public void DoAddComponentToElementListButton ()
		{
			if (GUILayout.Button("Add controller to elements", GUILayout.MaxWidth(200f)))
			{
				TailRigging.RigTail(tailRootRigger.tailRoot);
			}
		}
	//ENDOF Setup GUI layout

		private void GetElementListFromChildren ()
		{
			Debug.LogWarning("Tail editor method GetElementListFromChildren still uses BoneHierarchy.GetChildren Method. Danger of breaking upon updating GetChildren");
			tailRootRigger.tailRoot.elementList = BoneHierarchy.GetChildren(tailRootRigger.transform, recursive: true, includeIgnored: true).ToArray();
		}
*/
		
	}
}