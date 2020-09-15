using UnityEngine; //Physics, Transform, SpringJoint, ...

using static ASSPhysics.HandSystem.Actions.ActionSettings.ActionSettings; //tailGrabSettings, surfaceGrabSettings
using ASSistant.ComponentConfigurers; //SpringJoint2D.ApplySettings(sample);

namespace ASSPhysics.HandSystem.Actions
{
	public class ActionGrab : ActionBase
	{
	//ActionBase abstract implementation
		//returns true if this action is currently doing something, like maintaining a grab or repeating a slapping pattern
		//Will be true if base.ongoing (because automated) or if we have a joint list acting upon the world
		public override bool ongoing { get { return (jointList != null || base.ongoing); }}
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
	//ENDOF ActionBase abstract implementation

	//Grab Action Implementation
		//list of currently in-use joints
		private SpringJoint2D[] jointList;

		//initiate grabbing action
		private void InitiateGrab ()
		{
			CreateJoints(GetBonesInRange(), grabJointSettings.sampleJoint);
		}

		//End grabbing action
		private void FinishGrab ()
		{
			Clear();
		}

		//Gets all of the grabbable transforms. First tries to fetch a single tail backbone.
		//If no tail bones, fetch every surface bone in range
		private Transform[] GetBonesInRange ()
		{
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
				//QueryTriggerInteraction.Collide
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
		private void CreateJoints (Transform[] targets, SpringJoint2D sampleSpring)
		{
			//clear joint list and create a new list
			RemoveJoints();
			jointList = new SpringJoint2D[targets.Length];
			//create a joint for each target
			for (int i = 0, iLimit = targets.Length; i < iLimit; i++)
			{
				jointList[i] = CreateJoint(targets[i], sampleSpring);
			}
		}

		//Create a joint linked to a specific transform
		private SpringJoint2D CreateJoint (Transform target, SpringJoint2D sampleSpring)
		{
			//fetch target rigidbody and ensure it exists
			Rigidbody2D targetBody = target.GetComponent<Rigidbody2D>();
			if (targetBody == null) { return null; }
			//create the joint
			SpringJoint2D newJoint = tool.gameObject.AddComponent<SpringJoint2D>();
			//apply the sample settings and link target rigidbody
			newJoint.ApplySettings(sampleSpring);
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
