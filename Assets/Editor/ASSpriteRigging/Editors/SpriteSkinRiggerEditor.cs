using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using ASSpriteRigging.Riggers; //SpriteSkinBaseRigger, SpriteSkinRigger
using ASSpriteRigging.BoneUtility;

namespace ASSpriteRigging.Editors
{
	[CustomEditor(typeof(SpriteSkinRigger))]
	public class SpriteSkinRiggerEditor : RiggerEditorBase
	{
		protected override void RigBones ()
		{
			SpriteSkinRigging.RigBones((SpriteSkinRigger) rigger);
			Debug.Log("Rigged bones of " + target.name);
		}
	}
}