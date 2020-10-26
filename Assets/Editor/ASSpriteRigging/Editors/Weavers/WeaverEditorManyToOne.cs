using UnityEngine;
using UnityEditor;

using ASSpriteRigging.Weavers;

namespace ASSpriteRigging.Editors
{
	[CustomEditor(typeof(WeaverInspectorManyToOne))]
	public abstract class WeaverEditorManyToOne : WeaverEditorBase
	{
	//private fields and properties
		private WeaverInspectorManyToOne weaver { get { return (WeaverInspectorManyToOne) target; }}
	//ENDOF private fields and properties

	//private method declaration
		protected override void WeaveJoints()
		{
			Debug.LogError("WeaverManyToOneEditor.WeaveJoints(); unimplemented");
		}
	//ENDOF private method declaration
	}
}
