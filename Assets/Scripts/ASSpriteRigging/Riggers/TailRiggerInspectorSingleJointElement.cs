using UnityEngine;

using UnityEngine.U2D.Animation; //SpriteSkin

using IPulsePropagator = ASSPhysics.PulseSystem.PulsePropagators.IPulsePropagator;

using TailElementSingleJoint = ASSPhysics.TailSystem.TailElementSingleJoint;
//using ASSPhysics.TailSystem;	//TailRootWiggle

namespace ASSpriteRigging.Riggers
{
	[RequireComponent(typeof(SpriteSkin))]
	//[RequireComponent(typeof(TailRootWiggle))]
	public class TailRiggerInspectorSingleJointElement : TailRiggerInspectorBase
	{
		public TailElementSingleJoint defaultTailElement;	//default tail element controller
	}
}