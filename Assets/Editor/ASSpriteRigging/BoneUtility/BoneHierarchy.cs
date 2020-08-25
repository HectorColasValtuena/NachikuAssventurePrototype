using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASSpriteRigging.BoneUtility
{
	public static class BoneHierarchy
	{
		public static List<Transform> GetChildren (Transform root, bool recursive = true, bool includeIgnored = false)
		{
			List<Transform> childList = new List<Transform>();
			for (int i = 0, iLimit = root.childCount; i < iLimit; i++)
			{
				Transform child = root.GetChild(i);

				//ignore this element if it includes an ignore tag
				if (!includeIgnored && BoneNomenclature.IsIgnored(child)) { continue; }

				childList.Add(child);

				//add children of this element if necessary
				if (recursive) { childList.AddRange(GetChildren(child, recursive, includeIgnored));	}
			}
			return childList;
		}
	}
}