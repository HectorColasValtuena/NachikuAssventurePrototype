using UnityEngine;

namespace ASSPhysics.MiscellaneousComponents
{
	public class ParticleSystemPlayerOneShot : PlayerOneShotBase<ParticleSystem>
	{
		[SerializeField]
		private bool forceRestart = true;

		protected override void Play (ParticleSystem particleSystem)
		{
			if (forceRestart) { particleSystem.Stop(); }
			particleSystem.Play();
		}
	}
}