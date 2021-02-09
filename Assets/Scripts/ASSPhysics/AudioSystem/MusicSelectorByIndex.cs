using UnityEngine;

using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

namespace ASSPhysics.AudioSystem
{
	public class MusicSelectorByIndex : MonoBehaviour
	{
		[SerializeField]
		private int playOnAwake = -1;

		public void Awake ()
		{
			if (playOnAwake < 0 || ControllerCache.musicController == null) { return; }
			ControllerCache.musicController.Play(playOnAwake);
		}
	}
}