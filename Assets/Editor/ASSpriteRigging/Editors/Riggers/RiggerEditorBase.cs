using UnityEngine;
using UnityEditor;

using ASSpriteRigging.BoneUtility;
using ASSpriteRigging.Riggers; //SpriteSkinBaseRigger

namespace ASSpriteRigging.Editors
{
	public abstract class RiggerEditorBase<TInspector> : ArmableEditorBase<TInspector>
		where TInspector : SpriteSkinRiggerInspectorBase
	{
	//EditorBase implementation
		protected override void DoButtons ()
		{
			DoButton("Full setup", FullSetup);
			DoButton("Full setup", RigBones);
		}
	//ENDOF EditorBase implementation

	//private methods
		//performs every step of the automated rigging process at once:
		//moves ten units of sperm forwards, then cast whale at next 2 tiles unless hitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitler
		//that means:
			//> create gameobjects for every bone invoking the corresponding SpriteSkin methods
			//> rig default components for every corresponding bone gameobject (abstract- each rigger performs its own rigging)
		private void FullSetup ()
		{
			Debug.Log("Initiating full setup of " + targetInspector.name);
			BoneHierarchy.CreateBoneHierarchy(targetInspector);
			RigBones();
			Debug.Log("Full setup finished");
		}
	//ENDOF private methods

	//overridable methods and properties
		protected abstract void RigBones ();
	//ENDOF overridable methods
	}
}