using UnityEngine;

using PropagatorInspectorTransformTree = ASSpriteRigging.Inspectors.PropagatorInspectorTransformTree;

using IRiggerInspector = ASSpriteRigging.Inspectors.IRiggerInspector;
using IWeaverInspector = ASSpriteRigging.Inspectors.IWeaverInspector;
using IPropagatorInspector = ASSpriteRigging.Inspectors.IPropagatorInspector;

namespace ASSpriteRigging.Editors
{
	[UnityEditor.CustomEditor(typeof(PropagatorInspectorTransformTree))]
	public class PropagatorEditorTransformTree
	:
		PropagatorEditorBase<PropagatorInspectorTransformTree>
	{
	//[TO-DO] [IMPORTANT] Propagation should probably ignore disabled gameobjects

	//IPropagatorEditor implementation
	  //IEditorBase implementation
		public override void DoSetup ()
		{
			Debug.LogWarning(targetInspector.name + " > PropagatorEditorTransformTree.DoSetup() initiating...");
			RecursivelyApply(targetInspector.transform, ApplySetup);
			Debug.Log(targetInspector.name + " > PropagatorEditorTransformTree.DoSetup() done");
		}
	  //ENDOF IEditorBase implementation
		
	  //IEditorPurgeableBase implementation
		public override void DoPurge ()
		{
			Debug.LogWarning(targetInspector.name + " > PropagatorEditorTransformTree.DoPurge() initiating...");
			RecursivelyApply(targetInspector.transform, ApplyPurge);
			Debug.Log(targetInspector.name + " > PropagatorEditorTransformTree.DoPurge() done");
		}
	  //ENDOF IEditorPurgeableBase implementation

		public override void DoPropagate (PropagationApplicationDelegate apply)
		{
			RecursivelyApply(targetInspector.transform, apply);
		}
	//ENDOF IPropagatorEditor implementation

	//private methods
		//recursively propagate call
		private void RecursivelyApply (Transform root, PropagationApplicationDelegate apply)
		{
			for (int i = 0, iLimit = root.childCount; i < iLimit; i++)
			{
				Transform child = root.GetChild(i);

				//for each immediate child, apply propagation
				apply(child);

				//then attempt to propagate call to pre-existing propagators in target's children
				bool propagated = false;
				foreach (Component propagatorInspector in child.GetComponents<IPropagatorInspector>())
				{
					(CreateEditor(propagatorInspector) as IPropagatorEditor)?.DoPropagate(apply);
					propagated = true;
				}

				//if no propagator found, manually propagate recursive propagation
				if (!propagated)
				{
					RecursivelyApply(child, apply);
				}
			}
		}

		//Executes a Setup request on every child Rigger and Weaver
		private void ApplySetup (Transform root)
		{
			foreach (Component riggerInspector in root.GetComponents<IRiggerInspector>())
			{
				(CreateEditor(riggerInspector) as IRiggerEditor)?.DoSetup();
			}

			foreach (Component weaverInspector in root.GetComponents<IWeaverInspector>())
			{
				(CreateEditor(weaverInspector) as IWeaverEditor)?.DoSetup();	
			}
		}
		
		//Executes a Setup request on every child Rigger. Weavers do not purge
		private void ApplyPurge (Transform root)
		{
			foreach (Component riggerInspector in root.GetComponents<IRiggerInspector>())
			{
				(CreateEditor(riggerInspector) as IRiggerEditor)?.DoPurge();
			}
		}
	//ENDOF private methods
	}
}