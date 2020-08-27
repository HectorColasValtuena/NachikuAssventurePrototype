using UnityEngine;

namespace ASSpriteRigging
{
	public class SpriteSkinRigger : MonoBehaviour
	{
		public string baseBoneName = "bone_";
		public Sprite targetSprite;
		//public SpriteRenderer targetRenderer;

		public Rigidbody2D defaultRigidbody;
		public SpringJoint2D defaultAnchor;
		public Transform[] boneList;
	}
}