using UnityEngine;
using UnityEngine.U2D.Animation;

namespace ASSpriteRigging.Riggers
{
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(SpriteSkin))]
	public class SkinSurfaceRiggerInspector : SpriteSkinRiggerInspectorBase
	{
		public Rigidbody defaultRigidbody;	//Sample rigidbody configuration
		public SphereCollider defaultCollider;	//Sample collider configuration
		public ConfigurableJoint defaultAnchorJoint;	//Sample anchor joint (parent-connected) configuration
		public ConfigurableJoint defaultMeshJoint;		//Sample inter-vertex joint configuration
	}
}