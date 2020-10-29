using UnityEngine;
using UnityEditor;

using ASSpriteRigging.Weavers;

namespace ASSpriteRigging.Editors
{
	[CustomEditor(typeof(WeaverInspectorManyToOne))]
	public class WeaverEditorManyToOne : WeaverEditorBase<WeaverInspectorManyToOne>
	{
	//private method declaration
		protected override void WeaveJoints()
		{
			foreach (Rigidbody originRigidbody in targetInspector.originRigidbodyList)
			{
				ConnectRigidbodies(originRigidbody, targetInspector.targetRigidbody);
			}
			Debug.Log(targetInspector.name + " Weaved ManyToOne joints");
		}
	//ENDOF private method declaration
	}
}
