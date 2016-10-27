using UnityEngine;
using System.Collections;

namespace JCFramework{
	public class AudioManager : JCMonoSingleton<AudioManager> {
		private AudioSource soundAudio;
		private AudioSource bgmAudio;

		private float soundVolume = 1.0f;
		private float bgmVolume = 1.0f;

		private AudioManager(){
			soundAudio = gameObject.AddComponent<AudioSource> ();
			bgmAudio = gameObject.AddComponent<AudioSource> ();
		}

		public void PlaySound(string path, bool loop = false){
			AudioClip clip = ResourceManager.getInstance ().getAudio (path);
			soundAudio.volume = soundVolume;
			soundAudio.loop = loop;
			soundAudio.clip = clip;
			soundAudio.Play ();
		}

		public void PlayBgm(string path, bool loop = true){
			AudioClip bgm = ResourceManager.getInstance ().getAudio (path);
			bgmAudio.volume = bgmVolume;
			bgmAudio.loop = loop;
			bgmAudio.clip = bgm;
			bgmAudio.Play ();
		}
	}
}