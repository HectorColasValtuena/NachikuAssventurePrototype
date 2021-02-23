using UnityEngine;

namespace ASSpriteRigging.Inspectors
{
	public class WeaverInspectorManyToClosestChild : WeaverInspectorManyToXBase
	{
		public Transform[] targetRootTransformList = null;
		public bool includeRootTransform = false;
	}
}