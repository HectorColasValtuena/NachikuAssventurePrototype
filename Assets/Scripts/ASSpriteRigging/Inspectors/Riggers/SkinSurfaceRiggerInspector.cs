using UnityEngine;
using UnityEngine.U2D.Animation;

namespace ASSpriteRigging.Inspectors
{
	[RequireComponent(typeof(SpriteRenderer))]
	[RequireComponent(typeof(SpriteSkin))]
	public class SkinSurfaceRiggerInspector : SpriteSkinRiggerInspectorBase
	{
		public ConfigurableJoint defaultMeshJoint;		//Sample inter-vertex joint configuration
		public ConfigurableJoint defaultAnchorJoint;	//Sample anchor joint (parent-connected) configuration
	}
}