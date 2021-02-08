using UnityEngine;

using RandomRangeFloat = ASSistant.ASSRandom.RandomRangeFloat;

namespace ASSPhysics.AudioSystem
{
	//container object with settings for an audiosource playback
	[System.Serializable]
	public struct AudioPlaybackSettings
	{
		[SerializeField]
		public AudioClip clip;				//clip to play back
		[SerializeField]
		public RandomRangeFloat volume;		//volume modifier for this clip
		[SerializeField]
		public bool looping;				//should the clip loop
		[SerializeField]
		public RandomRangeFloat pitch;		//pitch
	}
}