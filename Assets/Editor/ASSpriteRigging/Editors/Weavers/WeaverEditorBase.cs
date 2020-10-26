using UnityEngine;
using UnityEditor;

using BoneRigging = ASSpriteRigging.BoneUtility.BoneRigging;
using WeaverInspectorBase = ASSpriteRigging.Weavers.WeaverInspectorBase;


namespace ASSpriteRigging.Editors
{
	//[CustomEditor(typeof(WeaverManyToOne))]
	public abstract class WeaverEditorBase<TInspector> : ArmableEditorBase<TInspector>
		where TInspector : WeaverInspectorBase
	{
	//ArmableEditorBase implementation
		protected override void DoButtons ()
		{
			DoButton("Setup weaving joints", WeaveJoints);
		}
	//ENDOF ArmableEditorBase implementation

	//private methods
		protected void ConnectRigidbodies (Rigidbody fromRigidbody, Rigidbody toRigidbody)
		{
			BoneRigging.BoneConnectJoint<ConfigurableJoint>(
				bone: fromRigidbody.transform,
				targetRigidbody: toRigidbody,
				sample: targetInspector.defaultJoint
			);
		}
	//ENDOF private methods

	//Abstract methods
		protected abstract void WeaveJoints ();
	//ENDOF Abstract methods
	}
}