using UnityEngine;

namespace ASSPhysics.MiscellaneousComponents
{
	public class AudioPlayerOneShot : MonoBehaviour
	{
		public AudioSource[] audioSources;

		public void PlayAll ()
		{
			foreach (AudioSource audioSource in audioSources)
			{
				audioSource.Play();
			}
		}

		public void PlayOne (int target)
		{
			audioSources[target].Play();
		}
	}
}