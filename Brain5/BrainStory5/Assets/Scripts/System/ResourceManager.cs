using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace JCFramework{
	public class ResourceManager : JCMonoSingleton<ResourceManager> {
		Dictionary<string, GameObject> m_dictPrefab;
		Dictionary<string, Texture> m_dictTexture;
		Dictionary<string, AudioClip> m_dictAudio;

		private ResourceManager(){
			m_dictPrefab = new Dictionary<string, GameObject>();
			m_dictTexture = new Dictionary<string, Texture> ();
			m_dictAudio = new Dictionary<string, AudioClip> ();
		}

		/*
		 * Get gameobject by path
		 * */
		public GameObject getPrefab(string path){
			if (m_dictPrefab.ContainsKey (path)) {
				return m_dictPrefab [path];
			} else {
				GameObject prefab = Resources.Load<GameObject> (path);
				if (prefab == null) {
					LogManager.getInstance ().Log ("Prefab: " + path + " not found.", LogLevel.Warning);
					return new GameObject(path);
				} else {
					m_dictPrefab [path] = prefab;
					return prefab;
				}
			}
		}

		/*
		 * Instantiate a gameobject by path
		 * */
		public GameObject createPrefab(string path){
			GameObject go = Instantiate (getPrefab (path)) as GameObject;
			return go;
		}

		/*
		 * Get texture by path
		 * */
		public Texture getTexture(string path){
			if (m_dictTexture.ContainsKey (path)) {
				return m_dictTexture [path];
			} else {
				Texture texture = Resources.Load<Texture> (path);
				if (texture == null) {
					LogManager.getInstance ().Log ("Texture: " + path + " not found.", LogLevel.Warning);
					return new Texture ();
				} else {
					m_dictTexture [path] = texture;
					return texture;
				}
			}
		}

		/*
		 * Get AudioClip by path
		 * */
		public AudioClip getAudio(string path){
			if (m_dictAudio.ContainsKey (path)) {
				return m_dictAudio [path];
			} else {
				AudioClip clip = Resources.Load<AudioClip> (path);
				if (clip == null) {
					LogManager.getInstance ().Log ("AudioClip: " + path + " not found.", LogLevel.Warning);
					return new AudioClip ();
				} else {
					m_dictAudio [path] = clip;
					return clip;
				}
			}
		}
	}
}