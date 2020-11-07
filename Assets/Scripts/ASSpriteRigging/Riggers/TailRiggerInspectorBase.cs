using UnityEngine;

namespace ASSpriteRigging.Riggers
{
	[RequireComponent(typeof(UnityEngine.U2D.Animation.SpriteSkin))]
	public class TailRiggerInspectorBase : SpriteSkinRiggerInspectorBase
	{
		public Rigidbody defaultRigidbody; //Sample rigidbody configuration
		public Collider defaultCollider;
		public Joint defaultChainJoint; //Sample spring configuration
	}
}