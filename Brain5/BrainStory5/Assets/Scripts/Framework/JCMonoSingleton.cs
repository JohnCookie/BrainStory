using UnityEngine;
using System.Collections;

namespace JCFramework{
	public abstract class JCMonoSingleton<T> : MonoBehaviour where T : JCMonoSingleton<T>{
		protected static T instance = null;

		public static T getInstance(){
			if (instance == null) {
				instance = FindObjectOfType<T> ();

				if (FindObjectsOfType<T> ().Length > 1) {
					return instance;
				}

				if (instance == null) {
					string instanceName = typeof(T).Name;

					GameObject go = GameObject.Find (instanceName);

					if (go == null) {
						go = new GameObject (instanceName);
					}
					instance = go.AddComponent<T> ();
					DontDestroyOnLoad (go);
				} else {
					
				}
			}
			return instance;

		}

		protected virtual void OnDestroy(){
			instance = null;
		}
	}
}