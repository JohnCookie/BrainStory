using UnityEngine;
using System.Collections;
using System;

public delegate void CDEndEventCallback();

public class CDEvent
{
	float startTime=0;
	float endTime=0;
	float currTime=0;
	CDEndEventCallback m_callback;

	public CDEvent(int start, int duration, CDEndEventCallback callback){
		startTime = (float)start;
		endTime = (float)(start + duration);
		currTime = (float)TimerHelper.getInstance().ConvertDateTimeInt(DateTime.Now);
		m_callback = callback;
	}

	public void updateTime(float deltaTime){
		currTime += deltaTime;
		if(currTime>=endTime){
			m_callback();
		}
	}

	public float getCurrProgress(){
		return (float)((endTime - startTime) / (currTime - startTime));
	}

	public string getLeftTimeStr(){
		int leftTime = (int)(endTime - currTime);
		DateTime dateTime = TimerHelper.getInstance ().GetTime (leftTime.ToString());
		int hour = dateTime.Hour;
		int minute = dateTime.Minute;
		int second = dateTime.Second;
		string hourStr = hour < 10 ? "0" + hour : hour.ToString ();
		string minuteStr = minute < 10 ? "0" + minute : minute.ToString ();
		string secondStr = second < 10 ? "0" + second : second.ToString ();
		return hourStr + ":" + minuteStr + ":" + secondStr;
	}

}

