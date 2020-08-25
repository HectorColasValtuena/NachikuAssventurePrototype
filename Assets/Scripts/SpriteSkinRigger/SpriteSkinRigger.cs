using UnityEngine;

namespace ASSPriteRigging
{
	public class SpriteSkinRigger : MonoBehaviour
	{
		public SpriteRenderer targetRenderer;

		public Rigidbody2D defaultRigidbody;
		public SpringJoint2D defaultAnchor;
		public Transform[] boneList;
	}
}