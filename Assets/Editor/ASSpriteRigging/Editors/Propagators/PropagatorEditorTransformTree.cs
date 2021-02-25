using UnityEngine;

using PropagatorInspectorTransformTree = ASSpriteRigging.Inspectors.PropagatorInspectorTransformTree;

namespace ASSpriteRigging.Editors
{
	[UnityEditor.CustomEditor(typeof(PropagatorInspectorTransformTree))]
	public class PropagatorEditorTransformTree
	:
		PropagatorEditorBase<PropagatorInspectorTransformTree>
	{
	//IPropagatorEditor implementation
	  //IEditorBase implementation
		public override void DoSetup ()
		{

		}
	  //ENDOF IEditorBase implementation
		
	  //IEditorPurgeableBase implementation
		public override void DoPurge ()
		{

		}
	  //ENDOF IEditorPurgeableBase implementation
	//ENDOF IPropagatorEditor implementation

	}
}