using UnityEngine;

namespace ASSpriteRigging.Weavers
{
	public class WeaverXToOneBase : WeaverBase
	{
		public Rigidbody connectedRigidbody;
		public Rigidbody targetRigidbody { get {
			//return this rigidbody if no target rigidbody is set
			return (connectedRigidbody != null)
				? connectedRigidbody
				: gameObject.GetComponent<Rigidbody>();
		}}
	}
}