namespace ASSpriteRigging.Editors
{
	public delegate void PropagationApplicationDelegate(UnityEngine.Transform propagationTarget);

	public interface IPropagatorEditor : IEditorPurgeableBase
	{
		//executes apply on every affected transform
		void DoPropagate (PropagationApplicationDelegate apply);
	}
}