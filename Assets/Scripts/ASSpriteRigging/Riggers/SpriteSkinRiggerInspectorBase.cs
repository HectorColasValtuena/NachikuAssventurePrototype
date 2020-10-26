using UnityEngine;
using UnityEngine.U2D.Animation;

namespace ASSpriteRigging.Riggers
{
	public class SpriteSkinRiggerInspectorBase : ASSpriteRigging.BaseInspectors.ArmableInspectorBase
	{
		public Rigidbody targetAnchor;
		public Rigidbody anchorRigidbody { get {
			//return this rigidbody if no target anchor is set
			return (targetAnchor != null)
				? targetAnchor
				: gameObject.GetComponent<Rigidbody>();
		}}
		
		public Sprite sprite { get { return gameObject.GetComponent<SpriteRenderer>()?.sprite; }}
		public SpriteSkin spriteSkin { get { return gameObject.GetComponent<UnityEngine.U2D.Animation.SpriteSkin>(); }}

		public GameObject defaultLayerSample; //gameobject with sample of layer tag
		public int defaultLayer { get { return (defaultLayerSample != null) ? defaultLayerSample.layer : -1; }}
		public string defaultTag { get { return defaultLayerSample?.tag; }}
		//public string defaultTag = "Grabbable";	//Which tag to set the bone transforms as
	}
}