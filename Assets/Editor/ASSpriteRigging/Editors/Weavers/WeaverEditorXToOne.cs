using UnityEngine;
using UnityEditor;

using ASSpriteRigging.Weavers;

namespace ASSpriteRigging.Editors
{
	[CustomEditor(typeof(WeaverInspectorXToOne))]
	public abstract class WeaverEditorXToOne : WeaverEditorBase
	{
	//private fields and properties
		private WeaverInspectorXToOne weaver { get { return (WeaverInspectorXToOne) target; }}
	//ENDOF private fields and properties

	//private method declaration
		protected override void WeaveJoints()
		{
			Debug.LogError("WeaverManyToOneEditor.WeaveJoints(); unimplemented");
		}
	//ENDOF private method declaration
	}
}
