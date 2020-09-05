using UnityEngine.U2D.Animation;

namespace U2DAnimationAccessor
{
	public static class SpriteSkinAccessor
	{
		public static void CreateBoneHierarchy (this SpriteSkin spriteSkin) { spriteSkin.CreateBoneHierarchy(); }
		public static void CalculateBounds (this SpriteSkin spriteSkin) { spriteSkin.CalculateBounds(); }
	}
}