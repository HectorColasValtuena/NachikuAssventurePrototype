using UnityEngine;

namespace ASSPhysics.MiscellaneousComponents
{
	public class AudioPlayerOneShot : PlayerOneShotBase<AudioSource>
	{
		protected override void Play (AudioSource audioSource)
		{
			audioSource.Play();
		}
	}
}