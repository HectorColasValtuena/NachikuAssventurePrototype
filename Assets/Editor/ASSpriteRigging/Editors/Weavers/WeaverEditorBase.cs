using UnityEngine;
using UnityEditor;

using BoneRigging = ASSpriteRigging.BoneUtility.BoneRigging;
using WeaverInspectorBase = ASSpriteRigging.Weavers.WeaverInspectorBase;


namespace ASSpriteRigging.Editors
{
	public abstract class WeaverEditorBase<TInspector> 
	:
		ArmableEditorBase<TInspector>,
		IWeaverEditor
		where TInspector : WeaverInspectorBase
	{
	//ArmableEditorBase implementation
		protected override void DoButtons ()
		{
			DoButton("Setup weaving joints", WeaveJoints);
		}
	//ENDOF ArmableEditorBase implementation

	//IWeaverEditor declaration
		public abstract void WeaveJoints ();
	//ENDOF IWeaverEditor declaration

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

	}
}