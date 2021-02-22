using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using ASSpriteRigging.Weavers;

namespace ASSpriteRigging.Editors
{
	[CustomEditor(typeof(WeaverInspectorManyToClosestChild))]
	public class WeaverEditorManyToClosestChild : WeaverEditorBase<WeaverInspectorManyToClosestChild>
	{
	//overriden inherited methods
		public override void WeaveJoints()
		{
		//value validation
			if (targetInspector.targetRootTransformList == null || targetInspector.targetRootTransformList.Length == 0)
			{
				Debug.Log("WeaverEditorManyToClosestChild.WeaveJoints() requires a list of root transforms");
				return;
			}

		//gather list of potential rigidbodies
			Rigidbody[] candidateRigidbodyList = FetchRigidbodyCandidates(
													rootTransforms: targetInspector.targetRootTransformList,
													includeRootTransform: targetInspector.includeRootTransform
												);
			/*Rigidbody[] targetRigidbodyList = targetInspector.targetRootTransform
											.GetComponentsInChildren<Rigidbody>(includeInactive: true);
			*/
		//find closest rigidbody for each rigidbody in originRigidbodyList
			foreach (Rigidbody originRigidbody in targetInspector.originRigidbodyList)
			{
				ConnectRigidbodies(
					fromRigidbody: originRigidbody,
					toRigidbody: FindClosestRigidbody(
						center: originRigidbody.transform.position,
						rigidbodyList: candidateRigidbodyList
					)
				);
			}
		}
	//ENDOF overriden inherited methods

	//private methods
		//fetchs all potential target rigidbodies
		private Rigidbody[] FetchRigidbodyCandidates (Transform[] rootTransforms, bool includeRootTransform = false)
		{
			List<Rigidbody> rigidbodyList = new List<Rigidbody>();
			foreach (Transform rootTransform in rootTransforms)
			{
				rigidbodyList.AddRange(
					rootTransform.GetComponentsInChildren<Rigidbody>(includeInactive: true)
				);	

				if (!includeRootTransform)
				{
					rigidbodyList.Remove(rootTransform.GetComponent<Rigidbody>());
				}
			}

			return rigidbodyList.ToArray();
		}

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