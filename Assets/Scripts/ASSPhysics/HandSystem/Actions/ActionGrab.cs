using UnityEngine; //Physics, Transform, SpringJoint, ...

using ASSPhysics.Constants;	//AnimationNames
using static ASSPhysics.HandSystem.Actions.ActionSettings.ActionSettings; //tailGrabSettings, surfaceGrabSettings
using EInputState = ASSPhysics.InputSystem.EInputState;

using ASSistant.ComponentConfiguration; //ComponentConfigurer.EMApplySettings(this Component);
										//ColliderPosition.EMGetColliderTransformOffset(this Collider);

namespace ASSPhysics.HandSystem.Actions
{
	public class ActionGrab : ActionBase
	{
	//ActionBase override implementation
		//returns true if this action is currently doing something, like maintaining a grab or repeating a slapping pattern
		//Will be true if base.ongoing (because automated) or if we have a joint list acting upon the world
		//receive state of corresponding input medium
		public override void Input (EInputState state)
		{
			if (state == EInputState.Started)
			{
				InitiateGrab();
			}
			if (state == EInputState.Ended)
			{
				FinishGrab();
			}
		}

		//clears and finishes the action
		public override void Clear ()
		{
			RemoveJoints();
			base.Clear();
		}

		//returns true if the action can be legally activated at its position
		public override bool IsValid ()
		{
/////////////////////////////////////////////////////////////////////////////////////////////////////
//[TO-DO] optimize initialization by keeping a copy of bone list?
//[TO-DO] consider OverlapCircleNonAlloc for fast validity checks too
/////////////////////////////////////////////////////////////////////////////////////////////////////			
			return (GetBoneCollidersInRange().Length > 0);
		}

		//try to set in automatic state. Returns true on success
		public override bool Automate ()
		{
			auto = grabActive;
			return auto;
		}

		//update automatic action. To be called once per frame while action is automated. returns false if automation stops
		public override bool AutomationUpdate ()
		{
			return auto;
		}

		public override void DeAutomate ()
		{
			auto = false;
		}
	//ENDOF ActionBase override implementation

	//Grab Action Implementation
		//list of currently in-use joints
		private ConfigurableJoint[] jointList;
		//determines if grab is active
		public bool grabActive { get { return (jointList != null); }}

		//initiate grabbing action
		private void InitiateGrab ()
		{
			CreateJoints(GetBoneCollidersInRange(), grabJointSettings.sampleJoint);
			tool.SetAnimationState(AnimationNames.Tool.stateGrab);
		}

		//End grabbing action
		private void FinishGrab ()
		{
			tool.SetAnimationState(null);
			Clear();
		}

		//Gets all of the grabbable transforms. First tries to fetch a single tail backbone.
		//If no tail bones, fetch every surface bone in range
		private Collider[] GetBoneCollidersInRange ()
		{
			Collider[] colliderList = tailGrabSettings.GetCollidersInRange(tool.transform);
			if (colliderList.Length < 1)
			{
				colliderList = surfaceGrabSettings.GetCollidersInRange(tool.transform);
			}
			return colliderList;
		}
	//ENDOF Grab Action Implementation

	//Grab Action support methods
		//create joints required from the tool gameobject to each target
		private void CreateJoints (Collider[] targets, ConfigurableJoint sampleSpring)
		{
			//clear joint list and create a new list
			RemoveJoints();
			jointList = new ConfigurableJoint[targets.Length];
			//create a joint for each target
			for (int i = 0, iLimit = targets.Length; i < iLimit; i++)
			{
				jointList[i] = CreateJoint(targets[i], sampleSpring);
			}
		}

		//Create a joint linked to a specific transform
		private ConfigurableJoint CreateJoint (Collider target, ConfigurableJoint sampleSpring)
		{
			//fetch target rigidbody and ensure it exists
			Rigidbody targetBody = target.GetComponent<Rigidbody>();
			if (targetBody == null) { return null; }
			//create the joint
			ConfigurableJoint newJoint = tool.gameObject.AddComponent<ConfigurableJoint>();
			//apply the sample settings and link target rigidbody
			newJoint.EMApplySettings<ConfigurableJoint>(sampleSpring);
			newJoint.connectedBody = targetBody;
			//set connection offset according to collider position
			newJoint.connectedAnchor = target.EMGetColliderTransformOffset();
			//return the component
			return newJoint;
		}

		//Remove all joints currently in use
		private void RemoveJoints ()
		{
			if (jointList != null)
			{
				for (int i = 0, iLimit = jointList.Length; i < iLimit; i++)
				{
					Object.Destroy(jointList[i]);
				}
				jointList = null;
			}
		}
	//ENDOF Grab Action support methods
	}
}
