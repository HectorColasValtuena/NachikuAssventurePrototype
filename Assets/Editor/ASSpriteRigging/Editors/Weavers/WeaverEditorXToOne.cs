using UnityEngine;
using UnityEditor;

using ASSpriteRigging.Weavers;

namespace ASSpriteRigging.Editors
{
	public abstract class WeaverEditorXToOne<TInspector> : WeaverEditorBase<TInspector>
		where TInspector : WeaverInspectorXToOneBase
	{
	//private method declaration
		protected void ConnectRigidbodyToTarget (Rigidbody toRigidbody)
		{
			ConnectRigidbodies(targetInspector.targetRigidbody, toRigidbody);
		}
	//ENDOF private method declaration
	}
}
