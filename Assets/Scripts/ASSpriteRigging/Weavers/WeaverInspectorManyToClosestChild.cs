using UnityEngine;

namespace ASSpriteRigging.Weavers
{
	public class WeaverInspectorManyToClosestChild : WeaverInspectorManyToXBase
	{
		public Transform targetRootTransform = null;
		public bool includeRootTransform = false;
	}
}