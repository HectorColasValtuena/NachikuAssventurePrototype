using IPulseData = ASSPhysics.PulseSystem.PulseData.IPulseData;

namespace ASSPhysics.TailSystem
{
	public class TailElementSimple : TailElementBase
	{
	//TailElementBase abstract method implementation
		//attempts to match current rotation with target rotation
		protected override void UpdateRotation (float timeDelta) {}
	//ENDOF TailElementBase abstract method implementation

	//IPulsePropagator abstract method implementation
				//execute a pulse and propagate it in the corresponding direction after proper delay	
		protected override void DoPulse (IPulseData pulseData) {}
	//ENDOF IPulsePropagator abstract method implementation
	}
}