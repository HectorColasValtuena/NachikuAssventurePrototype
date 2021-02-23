using UnityEngine;

namespace ASSpriteRigging.Editors
{
	public class PropagatorEditorBase<TInspector>
	:
		ArmableEditorBase<TInspector>,
		IPropagatorEditor
		where TInspector : IPropagatorInspector
	{
		
	}
}