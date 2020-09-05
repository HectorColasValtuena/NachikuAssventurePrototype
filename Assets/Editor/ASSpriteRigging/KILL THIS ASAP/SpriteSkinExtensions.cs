using UnityEngine;

using UnityEngine.U2D;				//SpriteSkin
using UnityEngine.U2D.Animation;	//SpriteBone,

namespace ASSpriteRigging.Unity2DAnimationEditorExtensions
{
	internal static class SpriteSkinExtensions
	{
		internal static void CreateBoneHierarchy(this SpriteSkin spriteSkin)
		{
			Sprite sprite = spriteSkin.GetComponent<SpriteRenderer>()?.sprite;
			if (sprite == null)
			{ throw new InvalidOperationException("SpriteRenderer has no Sprite set"); }

			var spriteBones = sprite.GetBones();
			var transforms = new Transform[spriteBones.Length];
			Transform root = null;

			for (int i = 0; i < spriteBones.Length; ++i)
			{
				CreateGameObject(i, spriteBones, transforms, spriteSkin.transform);
				if (spriteBones[i].parentId < 0 && root == null)
				{ root = transforms[i];}
			}

			spriteSkin.rootBone = root;
			spriteSkin.boneTransforms = transforms;
		}

		private static void CreateGameObject(int index, SpriteBone[] spriteBones, Transform[] transforms, Transform root)
		{
			if (transforms[index] == null)
			{
				var spriteBone = spriteBones[index];
				if (spriteBone.parentId >= 0)
				{ CreateGameObject(spriteBone.parentId, spriteBones, transforms, root); }

				var go = new GameObject(spriteBone.name);
				var transform = go.transform;
				if (spriteBone.parentId >= 0)
				{ transform.SetParent(transforms[spriteBone.parentId]); }
				else
				{ transform.SetParent(root); }
				transform.localPosition = spriteBone.position;
				transform.localRotation = spriteBone.rotation;
				transform.localScale = Vector3.one;
				transforms[index] = transform;
			}
		}
	}
}