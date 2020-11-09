using UnityEngine;
using UnityEditor;

//using ASSPhysics.TailSystem;	//TailRootWiggle, ITailElement
using BoneRigging = ASSpriteRigging.BoneUtility.BoneRigging;

using TInspector = ASSpriteRigging.Riggers.TailRiggerInspectorNoController;

using TElementController = ASSPhysics.TailSystem.TailElementJointSmoothFollow;

namespace ASSpriteRigging.Editors
{
//rigs a chain of bones with required components
	[CustomEditor(typeof(TInspector))]
	public class TailRiggerEditorNoController
		: TailRiggerEditorJointChainBase<TInspector>
	{
	}
}