using UnityEngine;
using UnityEditor;

using ASSpriteRigging.Weavers;

namespace ASSpriteRigging.Editors
{
	[CustomEditor(typeof(WeaverInspectorManyToOne))]
	public class WeaverEditorManyToOne : WeaverEditorXToOne<WeaverInspectorManyToOne>
	{
	//private method declaration
		protected override void WeaveJoints()
		{
			foreach (Rigidbody originRigidbody in targetInspector.originRigidbodyList)
			{
				ConnectRigidbodyToTarget(originRigidbody);
			}
			Debug.Log("Weaved ManyToOne joints");
		}
	//ENDOF private method declaration
	}
}
