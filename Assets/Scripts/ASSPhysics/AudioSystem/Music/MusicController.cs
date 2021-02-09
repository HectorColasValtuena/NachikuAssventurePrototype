using UnityEngine;

namespace ASSPhysics.AudioSystem.Music
{
//Manages music playback
	public class MusicController :
		ASSPhysics.ControllerSystem.MonoBehaviourControllerBase<IMusicController>,
		IMusicController
	{

	//private fields and properties
		//managed AudioSource component
		[SerializeField]
		private AudioSource audioSource = null;
		//serialized list of song presets
		[SerializeField]
		private AudioPlaybackProperties[] playbackPresets = null;

		//currently active playback
		private AudioPlaybackProperties currentPlayback;

		//currently generated playback volume
		private float playbackVolume = 1.0f;

		//easy getter for global volume settings 
		private float globalVolume
		{ get { /*[TO-DO]*/ return 1.0f; /*[TO-DO]*/ }} //////////////////////////////////////////////////////////////////////////////
 	//ENDOF private fields and properties

	//IMusicController implementation
		//set this to adjust fade in-out progress. Will stack with global sound settings and clip volume
		private float _fadeVolume = 1.0f;
		public float fadeVolume
		{
			get { return _fadeVolume; }
			set { _fadeVolume = value; }
		}

		//starts playback of desired track
		public void Play(int index)
		{ 
			if (playbackPresets == null || playbackPresets.Length == 0) { Debug.LogError("MusicController.Play(int): playbackPresets empty"); return; }
			Play(playbackPresets[index]);
		}
		public void Play(AudioPlaybackProperties properties)
		{
			//if requesting same song ignore request
			if (audioSource.isPlaying && currentPlayback.clip == properties.clip)
			{
				return;
			}

			currentPlayback = properties;
			audioSource.clip = properties.clip;
			audioSource.loop = properties.loop;
			audioSource.pitch = properties.pitch.Generate();
			playbackVolume = properties.volume.Generate();
			UpdateVolume();

			audioSource.Play();
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