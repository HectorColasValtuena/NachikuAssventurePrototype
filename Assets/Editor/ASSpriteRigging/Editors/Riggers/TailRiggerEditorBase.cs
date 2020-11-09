using UnityEngine;

namespace ASSpriteRigging.Editors
{
//rigs a chain of bones with required components
	public abstract class TailRiggerEditorBase<TInspector>
		: RiggerEditorBase<TInspector>
		where TInspector : ASSpriteRigging.Riggers.SpriteSkinRiggerInspectorBase
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
		private void RigTail (TInspector inspector = null)
		{	
			if (inspector == null) { inspector = targetInspector; }
			RigTailRoot(inspector.spriteSkin.rootBone, inspector);
			RigTailBoneElementRecursive(inspector.spriteSkin.rootBone, inspector);
		}

		//Recursively populate every transform with adequate controller and components
		private	void RigTailBoneElementRecursive (Transform bone, TInspector inspector)
		{
			Debug.Log("Rigging tail bone: "); Debug.LogWarning(bone);
			RigTailBone(bone, inspector);
			//loop over this element's transform children, recursively rigging each of them
			for (int i = 0, iLimit = bone.childCount; i < iLimit; i++)
			{
				Transform nextBone = bone.GetChild(i);
				//recursively rig each child so required rigidbodies exist
				RigTailBoneElementRecursive(nextBone, inspector);
				//finally create required joints between the elements
				RigTailBonePairConnection(bone, nextBone, inspector);
			}
		}
	//ENDOF private methods

	//abstract method declaration
		//rig the base/root of the transform chain
		protected abstract void RigTailRoot (Transform rootBone, TInspector inspector);

		//rig an individual element of the transform chain
		protected abstract void RigTailBone (Transform bone, TInspector inspector);

		//rig a connection between two elements
		protected abstract void RigTailBonePairConnection (Transform bone, Transform nextBone, TInspector inspector);
	//ENDOF abstract method declaration
	}
}