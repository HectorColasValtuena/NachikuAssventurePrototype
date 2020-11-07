using System.Reflection;

using UnityEngine;
using UnityEditor;

using ASSPhysics.TailSystem;	//TailRootWiggle, ITailElement
using ASSpriteRigging.Riggers;	//TailRigger
using BoneRigging = ASSpriteRigging.BoneUtility.BoneRigging;

using static ASSistant.ReflectionAssistant;

namespace ASSpriteRigging.Editors
{
//rigs a chain of bones with required components
	//[CustomEditor(typeof(TailRootRiggerInspectorBase))]
	public abstract class TailRootRiggerEditorBase<TTailRootRiggerInspector>
		: RiggerEditorBase<TTailRootRiggerInspector>
		where TTailRootRiggerInspector : TailRootRiggerInspectorBase
	{
	//inherited abstract method implementation
		protected override void RigBones ()
		{
			RigTail(targetInspector);
			Debug.Log("Rigged bones of " + targetInspector.name);
		}
	//ENDOF inherited abstract method implementation

	//private methods
		//extracts all the data from rigger object and calls a properly parametrized RigTailBoneRecursive
		private void RigTail (TTailRootRiggerInspector inspector)
		{	
			RigTailRoot(inspector.spriteSkin.rootBone, inspector);
			RigTailBoneElementRecursive(inspector.spriteSkin.rootBone, inspector);
		}

		//Recursively populate every transform with adequate controller and components
		private	void RigTailBoneElementRecursive (Transform bone, TTailRootRiggerInspector inspector)
		{
			Debug.Log("Rigging tail bone: "); Debug.LogWarning(bone);
			RigTailBoneElement(bone, inspector);
			//loop over this element's transform children, recursively rigging each of them
			for (int i = 0, iLimit = bone.childCount; i < iLimit; i++)
			{
				RigTailBoneElementRecursive(bone.GetChild(i), inspector);
			}
		}
	//ENDOF private methods


	//abstract method declaration
		protected abstract void RigTailRoot (Transform rootBone, TTailRootRiggerInspector inspector);
		protected abstract void RigTailBoneElement (Transform bone, TTailRootRiggerInspector inspector);
	}
}