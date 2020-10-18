using UnityEngine;
using UnityEditor;

using ASSpriteRigging.BoneUtility;
using ASSpriteRigging.Riggers; //SpriteSkinBaseRigger

namespace ASSpriteRigging.Editors
{
	public abstract class RiggerEditorBase : ArmableEditorBase
	{
	//private fields and properties
		protected SpriteSkinBaseRigger rigger;
	//ENDOF private fields and properties

	//ArmableEditorBase implementation
		protected override void InspectorInitialization ()
		{
			rigger = (SpriteSkinBaseRigger) target;			
		}

		protected override void DoButtons ()
		{
			DoFullSetupButton();
			DoRigBoneListButton();
		}
	//ENDOF ArmableEditorBase implementation

	//private methods
		public void DoFullSetupButton ()
		{
			if (GUILayout.Button("Full setup"))
			{
				if (RequestArmed()) { FullSetup(); }
			}
		}

		public void DoRigBoneListButton ()
		{
			if (GUILayout.Button("Setup & configure bone components"))//, GUILayout.MaxWidth(125f)))
			{
				if (RequestArmed()) { RigBones(); }
			}
		}

		//performs every step of the automated rigging process at once:
		//moves ten units of sperm forwards, then cast whale at next 2 tiles unless hitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitler
		//that means:
			//> create gameobjects for every bone invoking the corresponding SpriteSkin methods
			//> rig default components for every corresponding bone gameobject (abstract- each rigger performs its own rigging)
		private void FullSetup ()
		{
			Debug.Log("Initiating full setup of " + target.name);
			BoneHierarchy.CreateBoneHierarchy(rigger);
			RigBones();
			Debug.Log("Full setup finished");
		}
	//ENDOF private methods

	//overridable methods and properties
		protected abstract void RigBones ();
	//ENDOF overridable methods
	}
}