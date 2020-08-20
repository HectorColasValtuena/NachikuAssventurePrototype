using UnityEngine;

namespace SpriteRigging
{
	public class SpriteSkinRigger : MonoBehaviour
	{
		public Rigidbody2D defaultRigidbody;
		public SpringJoint2D defaultAnchor;
		public Transform[] boneList;
	}
}