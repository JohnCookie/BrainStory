using UnityEngine;
using System.Collections;

public delegate void DelayCallback();

public class TimerHelper : MonoBehaviour
{
	private static TimerHelper _instance;
	private TimerHelper(){

	}
	public static TimerHelper getInstance(){
		if (_instance == null) {
			_instance=GameObject.Find ("OtherHelpers").gameObject.GetComponent<TimerHelper> ();
			_instance.Init();		
		}
		return _instance;
	}

	void Init(){
		Debug.Log("TimerHelper Inited");
	}

	public void DelayFunc(float delayTime, DelayCallback callback){
		StartCoroutine(DelayAndRun(delayTime, callback));
	}

	IEnumerator DelayAndRun(float delayTime, DelayCallback callback)  
	{  
		yield return new WaitForSeconds(delayTime);  
		callback ();
	} 
}

