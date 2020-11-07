using UnityEngine;

using TailRiggerInspectorBase = ASSpriteRigging.Riggers.TailRiggerInspectorBase;

namespace ASSpriteRigging.Editors
{
//rigs a chain of bones with required components
	public abstract class TailRiggerEditorBase<TTailRootRiggerInspector>
		: RiggerEditorBase<TTailRootRiggerInspector>
		where TTailRootRiggerInspector : TailRiggerInspectorBase
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
		//rig the base/root of the transform chain
		protected abstract void RigTailRoot (Transform rootBone, TTailRootRiggerInspector inspector);

		//rig an individual element of the transform chain
		protected abstract void RigTailBoneElement (Transform bone, TTailRootRiggerInspector inspector);
	//abstract method declaration
	}
}