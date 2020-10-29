using UnityEngine;

namespace ASSpriteRigging.Weavers
{
	public class WeaverInspectorManyToOne : WeaverInspectorManyToXBase
	{
		public Rigidbody commonRigidbody;
		public Rigidbody targetRigidbody { get {
			//return this rigidbody if no target rigidbody is set
			return (commonRigidbody != null)
				? commonRigidbody
				: gameObject.GetComponent<Rigidbody>();
		}}
	}
}