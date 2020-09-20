using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ASSpriteRigging.Riggers; //SpriteSkinBaseRigger, TailRootRigger
using ASSpriteRigging.BoneUtility;

using ASSPhysics.TailSystem; //TailRootWiggle

namespace ASSpriteRigging.Editors
{
	[CustomEditor(typeof(TailRootRigger))]
	public class TailRootRiggerEditor : RiggerEditorBase
	{
		private TailRootRigger tailRootRigger;

	//Setup GUI layout
		protected override void InspectorInitialization ()
		{
			tailRootRigger = (TailRootRigger) target;
		}

		protected override void RigBones ()
		{
			//[TO-DO]
			Debug.Log("Rigged bones of " + target.name);
		}
	}
}