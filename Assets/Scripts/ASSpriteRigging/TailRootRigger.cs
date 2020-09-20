using UnityEngine;

using UnityEngine.U2D.Animation; //SpriteSkin

using ASSPhysics.TailSystem;	//TailRootWiggle

namespace ASSPriteRigging 
{
	[RequireComponent(typeof(SpriteSkin))]
	[RequireComponent(typeof(TailRootWiggle))]
	public class TailRootRigger : MonoBehaviour
	{
		public bool armed = false;

		//public Sprite sprite { get { return gameObject.GetComponent<SpriteRenderer>()?.sprite; }}
		public SpriteSkin spriteSkin { get { return gameObject.GetComponent<SpriteSkin>(); }}
		public TailRootWiggle tailRoot { get { return gameObject.GetComponent<TailRootWiggle>();}}
		//public Transform[] boneList { get { return spriteSkin?.; /*Lista de huesos?*/}}

		public GameObject defaultLayerSample; //gameobject with sample of layer tag
		public int defaultLayer { get { return (defaultLayerSample != null) ? defaultLayerSample.layer : -1; }}
		public string defaultTag = "Grabbable";	//Which tag to set the bone transforms as

		public Rigidbody2D defaultRigidbody; //Sample rigidbody configuration
		public CircleCollider2D defaultCollider;
		//public SpringJoint2D defaultAnchor;	//Sample anchor spring (parent-connected) configuration
		public SpringJoint2D defaultSpring; //Sample spring configuration
	}
}