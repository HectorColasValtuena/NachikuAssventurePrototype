using UnityEngine;
using SpriteSkin = UnityEngine.U2D.Animation.SpriteSkin;

namespace ASSpriteRigging.Inspectors
{
	public interface IRiggerInspector : IArmableInspector
	{
		//wether or not purging bone transform tree removes its rigidbodies too
		bool purgeKeepsRigidbodies { get; }

		//references to fundamental components
		Sprite sprite { get; }
		SpriteSkin spriteSkin { get; }

		//information on transform layer & tag
		int defaultLayer { get; }
		string defaultTag { get; }

		//Desired rigidbody configuration
		Rigidbody defaultRigidbody { get; }

		//Collider to include with each bone
		SphereCollider defaultCollider { get; }
	}
}