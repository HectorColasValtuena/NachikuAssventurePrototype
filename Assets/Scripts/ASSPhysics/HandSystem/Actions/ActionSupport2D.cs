using UnityEngine;

namespace ASSPhysics.HandSystem.Actions
{
	public static class ActionSupport2D
	{
	//CreateAnchoredJoint() Create an anchored joint linked to a specific target, adjusting offsets
		//overload taking a collider2d as 
		//public static 
		//
		public static TAnchoredJoint2D CreateAnchoredJoint2D <TAnchoredJoint2D> (
			Transform origin,
			Transform target,
			TAnchoredJoint2D sampleSpring,
			Vector2? originOffset = null,
			Vector2? targetOffset = null
		)
			where TAnchoredJoint2D: AnchoredJoint2D
		{
			return null;
		}
	//ENDOF CreateAnchoredJoint()

	//Private utility methods
		//Return the distance between the center of a collider and its anchored rigidbody as a vector2
		private static Vector2 GetColliderOffset (Collider2D collider)
		{
			return Vector2.zero;
		}
	//ENDOF Private utility methods
	}
}