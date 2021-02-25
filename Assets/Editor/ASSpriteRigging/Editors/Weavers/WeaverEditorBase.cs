using UnityEngine;
using UnityEditor;

using BoneRigging = ASSpriteRigging.BoneUtility.BoneRigging;
using IWeaverInspector = ASSpriteRigging.Inspectors.IWeaverInspector;


namespace ASSpriteRigging.Editors
{
	public abstract class WeaverEditorBase<TInspector> 
	:
		ArmableEditorBase<TInspector>,
		IWeaverEditor
		where TInspector : IWeaverInspector
	{
	//ArmableEditorBase implementation
		protected override void DoButtons ()
		{
			DoButton("Setup weaving joints", WeaveJoints);
		}
	//ENDOF ArmableEditorBase implementation

	//IWeaverEditor implementation
		public override void DoSetup ()
		{
			WeaveJoints();
		}
	//ENDOF IWeaverEditor implementation

	//private methods
		protected void ConnectRigidbodies (Rigidbody fromRigidbody, Rigidbody toRigidbody)
		{
			BoneRigging.BoneConnectJoint<ConfigurableJoint>(
				bone: fromRigidbody.transform,
				targetRigidbody: toRigidbody,
				sample: targetInspector.defaultWeavingJoint
			);
		}
	//ENDOF private methods

	//overridable methods
		public abstract void WeaveJoints();
	//ENDOF overridable methods
	}
}