using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ASSpriteRigging.Riggers; //SpriteSkinBaseRigger, SpriteSkinRigger
using ASSpriteRigging.BoneUtility;

namespace ASSpriteRigging.Editors
{
	[CustomEditor(typeof(SkinSurfaceRiggerInspector))]
	public class SkinSurfaceRiggerEditor : RiggerEditorBase<SkinSurfaceRiggerInspector>
	{
		protected override void RigBones ()
		{
			SkinSurfaceRigging.RigBones(targetInspector);
			Debug.Log("Rigged bones of " + targetInspector.name);
		}
	}
}