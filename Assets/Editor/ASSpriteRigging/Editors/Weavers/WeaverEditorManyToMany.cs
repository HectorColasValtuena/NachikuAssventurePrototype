using UnityEngine;
using UnityEditor;

using WeaverInspectorManyToMany = ASSpriteRigging.Inspectors.WeaverInspectorManyToMany;

namespace ASSpriteRigging.Editors
{
	[CustomEditor(typeof(WeaverInspectorManyToMany))]
	public class WeaverEditorManyToMany : WeaverEditorBase<WeaverInspectorManyToMany>
	{
	//private method declaration
		public override void WeaveJoints()
		{
			if (targetInspector.originRigidbodyList.Length != targetInspector.targetRigidbodyList.Length)
			{
				Debug.LogError("WeaverEditorManyToMany: Origin and Target rigidbody lists MUST be equal length");
				return;
			}

			for (int i = 0, iLimit = targetInspector.originRigidbodyList.Length; i < iLimit; i++)
			{
				ConnectRigidbodies(targetInspector.originRigidbodyList[i], targetInspector.targetRigidbodyList[i]);
			}
			Debug.Log(targetInspector.name + " Weaved ManyToMany joints");
		}
	//ENDOF private method declaration
	}
}
