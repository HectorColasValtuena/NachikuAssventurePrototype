using UnityEngine;
using UnityEditor;

using ASSpriteRigging.Weavers;

namespace ASSpriteRigging.Editors
{
	[CustomEditor(typeof(WeaverInspectorManyToClosestChild))]
	public class WeaverEditorManyToClosestChild : WeaverEditorBase<WeaverInspectorManyToClosestChild>
	{
	//overriden inherited methods
		protected override void WeaveJoints()
		{
		//value validation
			if (targetInspector.targetRootTransform == null)
			{
				Debug.Log("WeaverEditorManyToClosestChild.WeaveJoints() requires a rootTransform");
				return;
			}

		//gather list of potential rigidbodies
			Rigidbody[] targetRigidbodyList = targetInspector.targetRootTransform
											.GetComponentsInChildren<Rigidbody>(includeInactive: true);

		//find closest rigidbody for each rigidbody in originRigidbodyList
			foreach (Rigidbody originRigidbody in targetInspector.originRigidbodyList)
			{
				ConnectRigidbodies(
					fromRigidbody: originRigidbody,
					toRigidbody: FindClosestRigidbody(
						center: originRigidbody.transform.position,
						rigidbodyList: targetRigidbodyList
					)
				);
			}
		}
	//ENDOF overriden inherited methods

	//private methods
		//finds and returns the rigidbody closest to 0 among rigidbodyList
		private Rigidbody FindClosestRigidbody (Vector3 center, Rigidbody[] rigidbodyList)
		{
			if (rigidbodyList == null || rigidbodyList.Length == 0)
			{
				Debug.LogWarning("FindClosestRigidbody() no rigidbodyList provided or length 0");
				return null;
			}

			float closestDistance = float.MaxValue;
			Rigidbody closestRigidbody = null;
			for (int i = 0, iLimit = rigidbodyList.Length; i < iLimit; i++)
			{
				//ignore root transform if not included
				if (!targetInspector.includeRootTransform &&
					targetInspector.targetRootTransform == rigidbodyList[i].transform
				) {
					continue;
				}

				float distance = Vector3.Distance(center, rigidbodyList[i].transform.position);
				if (distance < closestDistance)
				{
						closestRigidbody = rigidbodyList[i];
						closestDistance = distance;
				}
			}

			return closestRigidbody;
		}
	//ENDOF private methods
	}
}