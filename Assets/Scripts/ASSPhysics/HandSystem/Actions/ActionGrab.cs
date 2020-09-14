using UnityEngine; //Physics, Transform, SpringJoint, ...

using static ASSPhysics.HandSystem.Actions.ActionSettings.ActionSettings; //tailGrabSettings, surfaceGrabSettings

namespace ASSPhysics.HandSystem.Actions
{
	public class ActionGrab : ActionBase
	{
	//ActionBase abstract implementation
		//receive state of corresponding input medium
		public override void Input (EInputState state)
		{

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
			return (GetBonesInRange().Length > 0);
		}
	//ENDOF ActionBase abstract implementation

	//ActionGrab specifics private implementation
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

		//get the single closest tail bone
		private Transform GetTailBoneInRange ()
		{
			//find all the tail bones nearby
			Collider[] boneList = Physics.OverlapSphere(
				tool.position,
				tailGrabSettings.radius,
				tailGrabSettings.grabbableMask,
				QueryTriggerInteraction.Collide
			);

			if (boneList.Length == 0) { return null; }

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
			return boneList[closest].transform;
		}

		//get a list of all surface bones in range
		private Transform[] GetSurfaceBonesInRange ()
		{
			Collider[] colliderList = Physics.OverlapSphere(
				tool.position,
				surfaceGrabSettings.radius,
				surfaceGrabSettings.grabbableMask,
				QueryTriggerInteraction.Collide
			);
			Transform[] transformList = new Transform[colliderList.Length];

			for (int i = 0, iLimit = colliderList.Length; i < iLimit; i++)
			{
				transformList[i] = colliderList[i].transform;
			}

			return transformList;
		}

		private SpringJoint[] jointList;

		private void CreateJoints (Transform[] targets, SpringJoint sampleTransform)
		{
			///////////////////////////////////////////////////////////////////////////////////////////////
			//[TO-DO]
			///////////////////////////////////////////////////////////////////////////////////////////////
		}

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
	//ENDOF ActionGrab specifics private implementation
	}
}
