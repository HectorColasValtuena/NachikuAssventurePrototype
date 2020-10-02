using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ASSpriteRigging.Riggers; //SpriteSkinBaseRigger, SpriteSkinRigger
using ASSpriteRigging.BoneUtility;

namespace ASSpriteRigging.Editors
{
	[CustomEditor(typeof(SkinSurfaceRigger))]
	public class SkinSurfaceRiggerEditor : RiggerEditorBase
	{
		protected override void RigBones ()
		{
			SkinSurfaceRigging.RigBones((SkinSurfaceRigger) rigger);
			Debug.Log("Rigged bones of " + target.name);
		}
	}
}