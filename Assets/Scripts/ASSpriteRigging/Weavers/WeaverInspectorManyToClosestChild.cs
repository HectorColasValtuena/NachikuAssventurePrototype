using UnityEngine;

namespace ASSpriteRigging.Weavers
{
	public class WeaverInspectorManyToClosestChild : WeaverInspectorManyToXBase
	{
		public Transform[] targetRootTransformList = null;
		public bool includeRootTransform = false;
	}
}