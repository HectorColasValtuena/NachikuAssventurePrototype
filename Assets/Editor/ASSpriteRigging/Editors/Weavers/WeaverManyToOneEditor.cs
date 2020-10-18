using UnityEngine;
using UnityEditor;

using WeaverBase = ASSpriteRigging.Weavers.WeaverBase;

namespace ASSpriteRigging.Editors
{
	[CustomEditor(typeof(WeaverManyToOne))]
	public abstract class WeaverManyToOneEditor : ArmableEditorBase
	{
	//private fields and properties
		protected WeaverManyToOne weaver;
	//ENDOF private fields and properties

	//ArmableEditorBase implementation
		protected override void InspectorInitialization ()
		{
			weaver = (WeaverManyToOne) target;			
		}

		protected override void DoButtons ()
		{
			if (GUILayout.Button("Setup weaving joints"))//, GUILayout.MaxWidth(125f)))
			{
				if (RequestArmed()) { WeaveJoints(); }
			}
		}
	//ENDOF ArmableEditorBase implementation

	//private method declaration
		private void WeaveJoints()
		{
			////////////////////////////////////////////////////////////////
			Debug.LogError("WeaverManyToOneEditor.WeaveJoints(); unimplemented");
		}
	//ENDOF private method declaration
	}
}
