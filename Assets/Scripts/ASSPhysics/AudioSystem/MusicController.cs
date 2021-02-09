using UnityEngine;

namespace ASSPhysics.AudioSystem
{
//Manages music playback
	public class MusicController :
		ASSPhysics.ControllerSystem.MonoBehaviourControllerBase<IMusicController>,
		IMusicController
	{

	//private fields and properties
		[SerializeField]
		private AudioSource audioSource = null;

		private AudioPlaybackProperties currentPlayback = null;

		private float playbackVolume
		{ get {}}
 	//ENDOF private fields and properties

	//IMusicController implementation

	//ENDOF IMusicController implementation

	//MonoBehaviour implementation
		public override void Awake ()
		{
			Base.Awake();
			if (audioSource == null) { audioSource = GetComponent<AudioSource>(); }
		}
	//ENDOF MonoBehaviour implementation
	}
}