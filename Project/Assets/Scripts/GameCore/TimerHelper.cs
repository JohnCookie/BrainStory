using UnityEngine;
using System.Collections;
using System;

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

	public DateTime GetTime(string timeStamp)
	{
		DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
		long lTime = long.Parse(timeStamp + "0000000");
		TimeSpan toNow = new TimeSpan(lTime); 
		return dtStart.Add(toNow);
	}

	public int ConvertDateTimeInt(System.DateTime time)
	{
		System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
		return (int)(time - startTime).TotalSeconds;
	}
}

