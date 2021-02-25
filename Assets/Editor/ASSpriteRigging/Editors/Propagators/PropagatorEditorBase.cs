using UnityEngine;

using IPropagatorInspector = ASSpriteRigging.Inspectors.IPropagatorInspector;

namespace ASSpriteRigging.Editors
{
	public abstract class PropagatorEditorBase<TInspector>
	:
		ArmableEditorBase<TInspector>,
		IPropagatorEditor
		where TInspector : UnityEngine.Object, IPropagatorInspector
	{
	//IPropagatorEditor implementation
	  //IEditorBase implementation
		//public override void DoSetup ()	//already implemented higher up 
	  //ENDOF IEditorBase implementation

	  //IEditorPurgeableBase implementation
		public abstract void DoPurge ();
	  //ENDOF IEditorPurgeableBase implementation
	//ENDOF IPropagatorEditor implementation
		
	}
}