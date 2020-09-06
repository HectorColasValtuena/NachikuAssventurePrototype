using UnityEngine;	//Bounds
using UnityEngine.U2D.Animation; //SpriteSkin

namespace U2DAnimationAccessor
{
	public static class SpriteSkinAccessor
	{
		//public static Transform[] GetBoneTransforms (this SpriteSkin spriteSkin) { return spriteSkin.m_BoneTransforms; }
		//public static Transform GetRootBone (this SpriteSkin spriteSkin) { return spriteSkin.m_RootBone; }

		public static void PublicCreateBoneHierarchy (this SpriteSkin spriteSkin) { spriteSkin.CreateBoneHierarchy(); }
		public static void CalculateBoundsIfNecessary (this SpriteSkin spriteSkin)
		{
			if (spriteSkin.isValid && spriteSkin.bounds == new Bounds())
			{
				spriteSkin.CalculateBounds();
			}
		}

	}
}