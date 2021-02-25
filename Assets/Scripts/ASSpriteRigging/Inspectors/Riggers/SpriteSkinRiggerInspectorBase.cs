using UnityEngine;
//using SpriteRenderer = UnityEngine.U2D.Animation.SpriteRenderer;
using SpriteSkin = UnityEngine.U2D.Animation.SpriteSkin;

namespace ASSpriteRigging.Inspectors
{
	[RequireComponent(typeof(SpriteSkin))]
	public abstract class SpriteSkinRiggerInspectorBase
	:
		ArmableInspectorBase,
		IRiggerInspector
	{
		//wether or not purging bone transform tree removes its rigidbodies too
		[SerializeField]
		private bool _purgeKeepsRigidbodies = true;
		public bool purgeKeepsRigidbodies { get { return _purgeKeepsRigidbodies; }}

		//[TO-DO]: move this declaration higher up in the hierarchy
		[SerializeField]
		private Rigidbody targetAnchor = null;
		public Rigidbody anchorRigidbody
		{
			get {
			//return this rigidbody if no target anchor is set
			return (targetAnchor != null)
				? targetAnchor
				: gameObject.GetComponent<Rigidbody>();
			}
		}
		
		public Sprite sprite { get { return gameObject.GetComponent<SpriteRenderer>()?.sprite; }}
		public SpriteSkin spriteSkin { get { return gameObject.GetComponent<SpriteSkin>(); }}

		//information on layer & tag
		[SerializeField]
		private GameObject defaultLayerSample = null;
		public int defaultLayer { get { return (defaultLayerSample != null) ? defaultLayerSample.layer : -1; }}
		public string defaultTag { get { return defaultLayerSample?.tag; }}


		//Desired rigidbody configuration
		[SerializeField]
		private Rigidbody _defaultRigidbody = null;
		public Rigidbody defaultRigidbody { get { return _defaultRigidbody; }}

		//Collider to include with each bone
		[SerializeField]
		private SphereCollider _defaultCollider = null;
		public SphereCollider defaultCollider { get { return _defaultCollider; }}
	}
}