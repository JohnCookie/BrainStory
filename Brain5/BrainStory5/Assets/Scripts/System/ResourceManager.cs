using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace JCFramework{
	public class ResourceManager : JCMonoSingleton<ResourceManager> {
		Dictionary<string, GameObject> m_dictPrefab;
		Dictionary<string, Texture> m_dictTexture;

		private ResourceManager(){
			m_dictPrefab = new Dictionary<string, GameObject>();
			m_dictTexture = new Dictionary<string, Texture> ();
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
	}
}