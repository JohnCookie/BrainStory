using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void ResourceLoadCallback(Object obj);

public class ResourceSystem : MonoBehaviour
{
	private static ResourceSystem _instance;
	public Dictionary<string, Object> mResDic = new Dictionary<string, Object>();

	private ResourceSystem(){
	}
	
	public static ResourceSystem getInstance(){
		if (_instance == null) {
			_instance=new ResourceSystem();
			_instance.Init();

		}
		return _instance;
	}

	void Init(){
		Debug.Log ("Resource System Inited.");
	}

	public void loadRes(string path, ResourceLoadCallback callback){
		Object obj = getResource (path);
		if (obj != null) {
			callback (obj);		
		} else {
			Debug.LogError("Can't Find Resource for "+path);
		}
	}

	private Object getResource(string path){
		if (IsResourceLoaded (path)) {
			return mResDic [path];		
		} else {
			Object res = Resources.Load(path);
			mResDic.Add(path, res);
			return res;
		}
	}

	private bool IsResourceLoaded(string path){
		if (mResDic.ContainsKey (path)) {
			return true;		
		}
		return false;
	}
}

