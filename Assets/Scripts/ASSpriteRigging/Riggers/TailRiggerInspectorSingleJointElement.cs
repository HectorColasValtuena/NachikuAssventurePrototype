using TailElementSingleJointChain = ASSPhysics.TailSystem.TailElementSingleJointChain;

namespace ASSpriteRigging.Riggers
{
	[UnityEngine.RequireComponent(typeof(UnityEngine.U2D.Animation.SpriteSkin))]
	public class TailRiggerInspectorSingleJointElement : TailRiggerInspectorJointChain
	{
		public TailElementSingleJointChain defaultTailElementController;	//default tail element controller
	}
}