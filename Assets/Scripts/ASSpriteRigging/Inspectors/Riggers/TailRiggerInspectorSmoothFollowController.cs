using TailElementJointSmoothFollow = ASSPhysics.TailSystem.TailElementJointSmoothFollow;

namespace ASSpriteRigging.Inspectors
{
	[UnityEngine.RequireComponent(typeof(UnityEngine.U2D.Animation.SpriteSkin))]
	public class TailRiggerInspectorSmoothFollowController : TailRiggerInspectorJointChain
	{
		public TailElementJointSmoothFollow defaultTailElementController;	//default tail element controller
	}
}