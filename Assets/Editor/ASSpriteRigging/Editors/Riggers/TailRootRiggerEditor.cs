using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ASSpriteRigging.Riggers; //SpriteSkinBaseRigger, TailRootRigger
using ASSpriteRigging.BoneUtility;

using ASSPhysics.TailSystem; //TailRootWiggle

namespace ASSpriteRigging.Editors
{
	[CustomEditor(typeof(TailRootRiggerInspector))]
	public class TailRootRiggerEditor : RiggerEditorBase<TailRootRiggerInspector>
	{
	protected override void RigBones ()
		{
			TailRigging.RigTail(targetInspector);
			Debug.Log("Rigged bones of " + targetInspector.name);
		}
	}
}