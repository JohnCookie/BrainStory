using UnityEngine;
using System.Collections;

public class TextUtils : MonoBehaviour
{
	private static TextUtils _instance;
	
	private TextUtils(){
	}
	
	public static TextUtils getInstance(){
		if (_instance == null) {
			_instance=GameObject.Find ("OtherHelpers").gameObject.GetComponent<TextUtils> ();
			_instance.Init();
			
		}
		return _instance;
	}
	
	void Init(){
		Debug.Log ("Resource System Inited.");
	}

	public string ReadTextFromResources(string filePath){
		TextAsset binAsset = Resources.Load(filePath,  typeof(TextAsset)) as TextAsset;    
		return binAsset.text;
	}


}

