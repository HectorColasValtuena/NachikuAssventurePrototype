using UnityEngine;
using UnityEditor;

using TInspector = ASSpriteRigging.Inspectors.TailRiggerInspectorNoController;

namespace ASSpriteRigging.Editors
{
	//rigs a chain of bones with required components
	[CustomEditor(typeof(TInspector))]
	public class TailRiggerEditorNoController
		: TailRiggerEditorJointChainBase<TInspector>
	{
	}
}