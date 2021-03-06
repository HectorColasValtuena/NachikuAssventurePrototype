﻿using UnityEngine;

namespace ASSPhysics.AudioSystem.Music
{
	public interface IMusicController : 
		ASSPhysics.ControllerSystem.IController
	{
		//starts playback of level track if not already playing
		void PlaySceneSong(int sceneIndex);
		//starts playback of desired track.
		//If forceRestart is true, attempting to play the same clip will restart playback
		//if fadeWithCurtain is true, song change will happen with a volume fade in-out synched with scene transition
		void PlaySong(AudioPlaybackProperties properties, bool forceRestart = false, bool fadeWithCurtain = false);
		//stops playback
		void Stop();
	}
}