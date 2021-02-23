using UnityEngine;

using IPropagatorInspector = ASSpriteRigging.Inspectors.IPropagatorInspector;

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