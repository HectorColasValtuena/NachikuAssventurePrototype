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
	//IPropagatorEditor declaration
	  //IEditorPurgeableBase declaration
		public abstract void DoPurge ();
	  //ENDOF IEditorPurgeableBase declaration

		public abstract void DoPropagate (PropagationApplicationDelegate apply);
	//ENDOF IPropagatorEditor declaration

	//EditorBase implementation
		protected override void DoButtons ()
		{
			DoButton("Propagate Rig Setup", DoSetup);
			//DoButton("Rig bone components & configuration", RigBones);
			DoButton("Disarm", Disarm);
			DoButton("Propagate Purge components", DoPurge);
		}
	//ENDOF EditorBase implementation
	}
}