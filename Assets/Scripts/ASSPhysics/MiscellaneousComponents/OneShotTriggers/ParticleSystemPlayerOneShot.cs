using UnityEngine;

namespace ASSPhysics.MiscellaneousComponents
{
	public class ParticleSystemPlayerOneShot : PlayerOneShotBase<ParticleSystem>
	{
		[SerializeField]
		private bool forceRestart = true;
		[SerializeField]
		private bool propagateToChildren = true;

		protected override void Play (ParticleSystem particleSystem)
		{
			if (forceRestart) { particleSystem.Stop(withChildren: propagateToChildren); }
			particleSystem.Play(withChildren: propagateToChildren);
		}
	}
}