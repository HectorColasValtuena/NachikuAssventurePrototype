using TailElementSingleJoint = ASSPhysics.TailSystem.TailElementSingleJoint;

namespace ASSpriteRigging.Riggers
{
	[UnityEngine.RequireComponent(typeof(UnityEngine.U2D.Animation.SpriteSkin))]
	public class TailRiggerInspectorSingleJointElement : TailRiggerInspectorBase
	{
		public TailElementSingleJoint defaultTailElement;	//default tail element controller
	}
}