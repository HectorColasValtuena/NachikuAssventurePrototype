using UnityEngine; //Physics, Transform, SpringJoint, ...

using static ASSPhysics.HandSystem.Actions.ActionSettings.ActionSettings; //tailGrabSettings, surfaceGrabSettings
using static ASSistant.ComponentConfiguration.ComponentConfigurer; //Component.ApplySettings(sample);
using AssPhysics.Constants;	//AnimationNames

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
			return (GetBonesInRange().Length > 0);
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
			CreateJoints(GetBonesInRange(), grabJointSettings.sampleJoint);
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
		private Transform[] GetBonesInRange ()
		{
			//!"()!")/()!""
	//replace GetTailBoneInRange and GetSurfaceBonesInRange with generic version
			Transform[] boneList = {GetTailBoneInRange()};
			if (boneList[0] == null)
			{
				boneList = GetSurfaceBonesInRange();
			}
			return boneList;
		}
	//ENDOF Grab Action Implementation

	//Grab Action support methods

		//get the single closest tail bone
		private Transform GetTailBoneInRange ()
/////////////////////////////////////////////////////////////////////////////////////////////////////
//[TO-DO] Congeal GetTailBoneInRange and GetSurfaceBonesInRange as a single parametrized method
/////////////////////////////////////////////////////////////////////////////////////////////////////
		{
			//find all the tail bones nearby
			Collider2D[] boneList = Physics2D.OverlapCircleAll(
				(Vector2) tool.position,
				tailGrabSettings.radius,
				tailGrabSettings.layerMask
				//QueryTriggerInteraction.Collide
			);
			if (boneList.Length == 0) {Debug.Log("GetTailBoneInRange found none"); return null; }

			//now find the nearest tail bone of the list
			int closest = 0;

			for (int i = 0, iLimit = boneList.Length; i < iLimit; i++)
			{
				//keep current bone if closer than previous closest
				if (
					Vector3.Distance(boneList[i].transform.position, tool.position)
					<
					Vector3.Distance(boneList[closest].transform.position, tool.position)
				) {
					closest = i;
				}
			}
			Debug.Log("GetTailBoneInRange found " + boneList[closest].transform);
			return boneList[closest].transform;
		}

		//get a list of all surface bones in range
		private Transform[] GetSurfaceBonesInRange ()
		{
			Collider2D[] colliderList = Physics2D.OverlapCircleAll(
				tool.position,
				surfaceGrabSettings.radius,
				surfaceGrabSettings.layerMask
			);
			Transform[] transformList = new Transform[colliderList.Length];

			for (int i = 0, iLimit = colliderList.Length; i < iLimit; i++)
			{
				transformList[i] = colliderList[i].transform;
			}

			Debug.Log("GetSurfaceBonesInRange found " + transformList.Length);
			return transformList;
		}


		//create joints required from the tool gameobject to each target
		private void CreateJoints (Transform[] targets, ConfigurableJoint sampleSpring)
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
		private ConfigurableJoint CreateJoint (Transform target, ConfigurableJoint sampleSpring)
		{
			//fetch target rigidbody and ensure it exists
			Rigidbody targetBody = target.GetComponent<Rigidbody>();
			if (targetBody == null) { return null; }
			//create the joint
			ConfigurableJoint newJoint = tool.gameObject.AddComponent<ConfigurableJoint>();
			//apply the sample settings and link target rigidbody
			newJoint.ApplySettings<ConfigurableJoint>(sampleSpring);
			newJoint.connectedBody = targetBody;
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
