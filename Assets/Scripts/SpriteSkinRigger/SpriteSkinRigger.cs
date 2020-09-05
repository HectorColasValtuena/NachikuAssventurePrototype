using UnityEngine;
using UnityEngine.U2D.Animation;

namespace ASSpriteRigging
{
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(SpriteSkin))]
	public class SpriteSkinRigger : MonoBehaviour
	{
		public bool armed = false;

		public Sprite targetSprite { get { return gameObject.GetComponent<SpriteRenderer>()?.sprite; }}
		public SpriteSkin spriteSkin { get { return gameObject.GetComponent<UnityEngine.U2D.Animation.SpriteSkin>(); }}

		public Rigidbody2D defaultRigidbody;
		public SpringJoint2D defaultAnchor;
		public Transform[] boneList;
	}
}