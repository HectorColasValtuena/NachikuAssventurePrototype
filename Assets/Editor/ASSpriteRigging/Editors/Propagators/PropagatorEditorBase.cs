using UnityEngine;

using IPropagatorInspector = ASSpriteRigging.Inspectors.IPropagatorInspector;

namespace ASSpriteRigging.Editors
{
	public abstract class PropagatorEditorBase<TInspector>
	:
		ArmableEditorBase<TInspector>,
		IPropagatorEditor
		where TInspector : IPropagatorInspector
	{
	//IPropagatorEditor implementation
	  //IEditorBase implementation
		public override void DoSetup ()
		{
			//[TO-DO]
			Debug.LogError("PropagatorEditorBase UNIMPLEMENTED");
		}
	  //ENDOF IEditorBase implementation

	  //IEditorPurgeableBase implementation
		public void DoPurge ()
		{
			//[TO-DO]
			Debug.LogError("PropagatorEditorBase UNIMPLEMENTED");
		}
	  //ENDOF IEditorPurgeableBase implementation
	//ENDOF IPropagatorEditor implementation
		
	}
}