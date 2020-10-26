using UnityEngine;
using UnityEditor;

using ASSpriteRigging.Weavers;

namespace ASSpriteRigging.Editors
{
	[CustomEditor(typeof(WeaverInspectorManyToOne))]
	public abstract class WeaverEditorManyToOne : WeaverEditorXToOne<WeaverInspectorManyToOne>
	{
	//private method declaration
		protected override void WeaveJoints()
		{
			Debug.LogError("WeaverManyToOneEditor.WeaveJoints(); unimplemented");
		}
	//ENDOF private method declaration
	}
}
