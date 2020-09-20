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
		protected override bool isArmed { get { return spriteSkinRigger.armed; } set { spriteSkinRigger.armed = value; }}
		private SpriteSkinRigger spriteSkinRigger;

		protected override void InspectorInitialization ()
		{
			spriteSkinRigger = (SpriteSkinRigger) target;
		}

		//performs every step of the automated rigging process at once:
		//moves ten units of sperm forwards, then cast whale at next 2 tiles unless hitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitler
		//that means:
			//> create gameobjects for every bone
			//> rig default components for every corresponding bone gameobject
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
	}
}