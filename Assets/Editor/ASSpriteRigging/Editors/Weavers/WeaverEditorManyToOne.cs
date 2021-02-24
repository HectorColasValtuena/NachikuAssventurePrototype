using UnityEngine;
using UnityEditor;

using WeaverInspectorManyToOne = ASSpriteRigging.Inspectors.WeaverInspectorManyToOne;

namespace ASSpriteRigging.Editors
{
	[CustomEditor(typeof(WeaverInspectorManyToOne))]
	public class WeaverEditorManyToOne : WeaverEditorBase<WeaverInspectorManyToOne>
	{
	//private method declaration
		public override void WeaveJoints()
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
