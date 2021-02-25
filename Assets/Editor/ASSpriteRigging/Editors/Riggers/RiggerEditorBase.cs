using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using SpriteSkin = UnityEngine.U2D.Animation.SpriteSkin;

using BoneHierarchy = ASSpriteRigging.BoneUtility.BoneHierarchy;
using IRiggerInspector = ASSpriteRigging.Inspectors.IRiggerInspector;

namespace ASSpriteRigging.Editors
{
	public abstract class RiggerEditorBase<TInspector>
	:
		ArmableEditorBase<TInspector>,
		IRiggerEditor
		where TInspector : UnityEngine.Object, IRiggerInspector
	{
	//EditorBase implementation
		protected override void DoButtons ()
		{
			DoButton("Full setup", FullSetup);
			//DoButton("Rig bone components & configuration", RigBones);
			DoButton("Disarm", Disarm);
			DoButton("Purge components", Purge);
		}
	//ENDOF EditorBase implementation

	//IRiggerEditor implementation
		public override void DoSetup ()
		{
			FullSetup();
		}

		public void DoPurge ()
		{
			Purge();
		}
	//ENDOF IRiggerEditor implementation

	//private methods
		//performs every step of the automated rigging process at once:
		//moves ten units of sperm forwards, then cast whale at next 2 tiles unless hitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitlerhitler
		//that means:
			//> create gameobjects for every bone invoking the corresponding SpriteSkin methods
			//> rig default components for every corresponding bone gameobject (abstract- each rigger performs its own rigging)
		private void FullSetup ()
		{
			Debug.Log("Initiating full setup of " + targetInspector.name);
			BoneHierarchy.CreateBoneHierarchy(targetInspector);
			RigBones();
			Debug.Log(targetInspector.name + " full setup finished");
		}

		private void Purge ()
		{
			SpriteSkin targetSpriteSkin = targetInspector.spriteSkin;

			if (targetSpriteSkin == null)
			{
				Debug.Log("No spriteSkin found in transform " + targetInspector.name);
				return;
			}

			foreach (Transform boneTransform in targetSpriteSkin.boneTransforms)
			{
				PurgeBonePhysicsComponents(boneTransform, targetInspector.purgeKeepsRigidbodies);
			}

			Debug.Log("Purged components");
		}

		private void PurgeBonePhysicsComponents (Transform boneTransform, bool keepRigidbodies = true)
		{
			//collect physics-related components
			List<Component> componentList = new List<Component>();

				//include colliders and joints
			componentList.AddRange(boneTransform.GetComponents<Collider>());
			componentList.AddRange(boneTransform.GetComponents<Joint>());

			//the remove every component included
			foreach (Component component in componentList)
			{
				Object.DestroyImmediate(component);
			}

			//last, attempt to remove rigidbody if necessary
			if (!keepRigidbodies)
			{
				Object.DestroyImmediate(boneTransform.GetComponent<Rigidbody>());
			}
		}
	//ENDOF private methods

	//overridable methods and properties
		protected abstract void RigBones ();
	//ENDOF overridable methods
	}
}