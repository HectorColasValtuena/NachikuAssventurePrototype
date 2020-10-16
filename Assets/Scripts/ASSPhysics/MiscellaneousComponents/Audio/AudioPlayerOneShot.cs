using UnityEngine;

namespace ASSPhysics.MiscellaneousComponents
{
	public class AudioPlayerOneShot : MonoBehaviour
	{
		public AudioSource[] audioSources;

		public void PlayAudioOneShot ()
		{
			foreach (AudioSource audioSource in audioSources)
			{
				audioSource.Play();
			}
		}
	}
}