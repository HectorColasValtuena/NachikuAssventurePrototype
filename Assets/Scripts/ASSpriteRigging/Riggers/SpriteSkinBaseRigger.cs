using UnityEngine;
using UnityEngine.U2D.Animation;

namespace ASSpriteRigging.Riggers
{
	public class SpriteSkinBaseRigger : MonoBehaviour
	{
		public bool armed = false;

		public Sprite sprite { get { return gameObject.GetComponent<SpriteRenderer>()?.sprite; }}
		public SpriteSkin spriteSkin { get { return gameObject.GetComponent<UnityEngine.U2D.Animation.SpriteSkin>(); }}

		public GameObject defaultLayerSample; //gameobject with sample of layer tag
		public int defaultLayer { get { return (defaultLayerSample != null) ? defaultLayerSample.layer : -1; }}
		public string defaultTag = "Grabbable";	//Which tag to set the bone transforms as
	}
}