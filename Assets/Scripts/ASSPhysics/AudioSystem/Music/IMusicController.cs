using UnityEngine;

namespace ASSPhysics.AudioSystem.Music
{
	public interface IMusicController : 
		ASSPhysics.ControllerSystem.IController
	{
		//?volume?
		//set this to adjust fade in-out progress. Will stack with global sound settings and clip volume
		float fadeVolume {get; set;}

		//starts playback of desired track
		void Play(int index);
		void Play(AudioPlaybackProperties properties);
		//stops playback
		void Stop();
	}
}