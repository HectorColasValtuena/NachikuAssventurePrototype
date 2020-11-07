using System.Reflection;

using UnityEngine;
using UnityEditor;

using ASSPhysics.TailSystem;	//TailRootWiggle, ITailElement
//using ASSpriteRigging.Riggers;	//TailRigger
using BoneRigging = ASSpriteRigging.BoneUtility.BoneRigging;

using static ASSistant.ReflectionAssistant;

using TInspector = ASSpriteRigging.Riggers.TailRiggerInspectorSingleJointElement;

namespace ASSpriteRigging.Editors
{
//rigs a chain of bones with required components
	//[CustomEditor(typeof(TailRootRiggerInspectorBase))]
	public abstract class TailRiggerEditorSingleJointElement
		: TailRiggerEditorBase<TInspector>
	{
	//abstract method implementation
		protected override void RigTailRoot (Transform rootBone, TInspector inspector)
		{
			
		}

		protected override void RigTailBoneElement (Transform bone, TInspector inspector)
		{

		}
	//ENDOF abstract method implementation
	}
}