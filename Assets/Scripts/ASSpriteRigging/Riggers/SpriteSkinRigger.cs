using UnityEngine;
using UnityEngine.U2D.Animation;

namespace ASSpriteRigging.Riggers
{
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(SpriteSkin))]
	public class SpriteSkinRigger : SpriteSkinBaseRigger
	{
		public Rigidbody2D defaultRigidbody; //Sample rigidbody configuration
		public CircleCollider2D defaultCollider;
		public SpringJoint2D defaultAnchor;	//Sample anchor spring (parent-connected) configuration
		public SpringJoint2D defaultSpring; //Sample spring configuration
	}
}