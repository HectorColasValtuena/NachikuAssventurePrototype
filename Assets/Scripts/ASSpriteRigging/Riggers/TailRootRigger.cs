using UnityEngine;

using UnityEngine.U2D.Animation; //SpriteSkin

using ASSPhysics.TailSystem;	//TailRootWiggle

namespace ASSpriteRigging.Riggers
{
	[RequireComponent(typeof(SpriteSkin))]
	[RequireComponent(typeof(TailRootWiggle))]
	public class TailRootRigger : SpriteSkinBaseRigger
	{
		public TailRootWiggle tailRoot { get { return gameObject.GetComponent<TailRootWiggle>();}}
		
		public Rigidbody2D defaultRigidbody; //Sample rigidbody configuration
		public CircleCollider2D defaultCollider;
		//public SpringJoint2D defaultAnchor;	//Sample anchor spring (parent-connected) configuration
		public SpringJoint2D defaultSpring; //Sample spring configuration
	}
}