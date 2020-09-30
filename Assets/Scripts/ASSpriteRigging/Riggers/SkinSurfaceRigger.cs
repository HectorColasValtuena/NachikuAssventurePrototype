using UnityEngine;
using UnityEngine.U2D.Animation;

namespace ASSpriteRigging.Riggers
{
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(SpriteSkin))]
	public class SkinSurfaceRigger : SpriteSkinBaseRigger
	{
		public Rigidbody defaultRigidbody;	//Sample rigidbody configuration
		public Collider defaultCollider;	//Sample collider configuration
		public Joint defaultAnchorJoint;	//Sample anchor joint (parent-connected) configuration
		public Joint defaultMeshJoint;		//Sample inter-vertex joint configuration
	}
}