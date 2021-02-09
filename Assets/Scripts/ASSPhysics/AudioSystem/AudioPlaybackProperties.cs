using UnityEngine;

using RandomRangeFloat = ASSistant.ASSRandom.RandomRangeFloat;

namespace ASSPhysics.AudioSystem
{
	//container object with settings for an audiosource playback
	[System.Serializable]
	public struct AudioPlaybackProperties
	{
		[SerializeField]
		public AudioClip clip = null;				//clip to play back
		[SerializeField]
		public RandomRangeFloat volume = null;		//volume modifier for this clip
		[SerializeField]
		public bool loop = false;				//should the clip loop
		[SerializeField]
		public RandomRangeFloat pitch = null;		//pitch

		public AudioPlaybackProperties (
			AudioClip _clip,
			RandomRangeFloat _volume,
			bool _loop,
			RandomRangeFloat _pitch
		) {
			clip = _clip;
			volume = _volume;
			loop = _loop;
			pitch = _pitch;

		}
	}
}