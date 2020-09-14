using UnityEngine; //Physics, Transform, SpringJoint, ...

using ASSPhysics.HandSystem.Actions.ActionSettings;

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
			Transform[] boneList = GetTailBoneInRange();
			if (boneList == null || boneList.Length == 0)
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
				Vector3 position,
				ActionSettings.tailGrabSettings.radius,
				ActionSettings.tailGrabSettings.grabbableMask,
				QueryTriggerInteraction.Collide
			);

			if (boneList.Length == 0) { return; }

			//now find the nearest tail bone of the list
			int closest = 0;

			for (int i = 0, iLimit = boneList.Length; i < iLimit; i++)
			{
				//keep current bone if closer than previous closest
				if (
					Vector3.distance(boneList[i].transform.position, tool.transform.position)
					<
					Vector3.distance(boneList[closest].transform.position, tool.transform.position)
				) {
					closest = i;
				}
			}
			return boneList[closest].transform;
		}

		//get a list of all bones in range
		private Transform[] GetSurfaceBonesInRange ()
		{
			return (Transform[]) Physics.OverlapSphere(
				Vector3 position,
				ActionSettings.surfaceGrabSettings.radius,
				ActionSettings.surfaceGrabSettings.grabbableMask,
				QueryTriggerInteraction.Collide
			);
		}

		private SpringJoint[] jointList;

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
