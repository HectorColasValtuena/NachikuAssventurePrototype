using UnityEngine;

using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

namespace ASSPhysics.AudioSystem.Music
{
	public class MusicFadeSetter : MonoBehaviour
	{
		[Range(0f, 1f)]
		public float musicFadeVolume;

		public void Update ()
		{
			ControllerCache.musicController.fadeVolume = musicFadeVolume;
		}
	}
}