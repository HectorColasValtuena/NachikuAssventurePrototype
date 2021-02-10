using UnityEngine;

using IEnumerator = System.Collections.IEnumerator;

using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

namespace ASSPhysics.AudioSystem.Music
{
//Manages music playback
	public class MusicController :
		ASSPhysics.ControllerSystem.MonoBehaviourControllerBase<IMusicController>,
		IMusicController
	{
	//private fields
		//managed AudioSource component
		[SerializeField]
		private AudioSource audioSource = null;
		//serialized list of song presets
		[SerializeField]
		private AudioPlaybackProperties[] scenePresets = null;

		//currently active playback
		private AudioPlaybackProperties currentPlayback = null;

		//currently generated playback volume
		private float playbackVolume = 1.0f;

		//fields used by transitions
		private bool inTransition { get { return transitionToPlayback != null; }}
		private AudioPlaybackProperties transitionToPlayback = null;
 	//ENDOF private fields

	//private properties
		//easy getter for global volume settings 
		private float globalVolume
		{ get { /*[TO-DO]*/ return 1.0f; /*[TO-DO]*/ }} //////////////////////////////////////////////////////////////////////////////
	//ENDOF private properties

	//IMusicController implementation
		//set this to adjust fade in-out progress. Will stack with global sound settings and clip volume
		private float fadeVolume = 1.0f;
		/*public float fadeVolume
		{
			get { return _fadeVolume; }
			private set { _fadeVolume = value; }
		}*/

		//starts playback of level track if not already playing
		public void PlaySceneSong(int sceneIndex)
		{ 
			if (scenePresets == null || scenePresets.Length == 0) { Debug.LogError("MusicController.PlaySceneSong(int): scenePresets empty"); return; }
			if (sceneIndex < 0 || sceneIndex >= scenePresets.Length) { Debug.LogError("MusicController.PlaySceneSong(int): can't take value: " + sceneIndex); return; }
			PlaySong(
				properties:scenePresets[sceneIndex],
				forceRestart: true,
				fadeWithCurtain: true
			);
		}

		//starts playback of desired track.
		//If forceRestart is true, attempting to play the same clip will restart playback
		//if fadeWithCurtain is true, song change will happen with a volume fade in-out synched with scene transition
		public void PlaySong (
			AudioPlaybackProperties properties,
			bool forceRestart = false,
			bool fadeWithCurtain = false
		) {
			//cancel playback if no audio clip
			if (properties == null || properties.clip == null) { Debug.LogError("MusicController.PlaySong(properties): properties null or contains null clip"); return; }
			
			//if requesting same song ignore request
			if (audioSource.isPlaying && forceRestart && currentPlayback.clip == properties.clip) { return; }

			StartCoroutine(PlaySongCoroutine());

			IEnumerator PlaySongCoroutine ()
			{
				//ensure no stacked transitions
				if (inTransition)
				{
					Debug.LogWarning("PlaySongCoroutine(): can't play new song, already waiting for transition");
					yield break;
				};

				transitionToPlayback = properties;

				//wait for song fade-out
				if (fadeWithCurtain)
				{
					while (
						!ControllerCache.curtainController.isCompletelyClosed
						//&& ControllerCache.curtainController.openingProgress > 0f
					) {
						fadeVolume = ControllerCache.curtainController.openingProgress;
						yield return null;
					}
				}

				//swap song
				SetSong();

				//wait for song fade-in
				if (fadeWithCurtain)
				{
					while (ControllerCache.curtainController.openingProgress < 1.0f)
					{
						fadeVolume = ControllerCache.curtainController.openingProgress;
						yield return null;
					}
				}

				//done
				fadeVolume = 1.0f;
				transitionToPlayback = null;
			}

			void SetSong ()
			{
				currentPlayback = properties;
				audioSource.clip = properties.clip;
				audioSource.loop = properties.loop;
				audioSource.pitch = properties.pitch.Generate();
				playbackVolume = properties.volume.Generate();
				UpdateVolume();
				audioSource.Play();
			}
		}

		//stops playback
		public void Stop()
		{
			audioSource.Stop();
		}
	//ENDOF IMusicController implementation

	//MonoBehaviour implementation
		public override void Awake ()
		{
			base.Awake();
			if (audioSource == null) { audioSource = GetComponent<AudioSource>(); }
		}

		public void Update ()
		{
			UpdateVolume();
		}
	//ENDOF MonoBehaviour implementation

	//private method implementation
		//updates player volume acording to volume settings, fade value, and playback properties
		private void UpdateVolume ()
		{
			audioSource.volume = globalVolume * fadeVolume * playbackVolume;
		}
	//ENDOF private method implementation
	}
}