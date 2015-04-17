using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public enum ReportActionType{
	Locate = 1,
	Move = 2,
	Attack = 3,
	Cast = 4,
	Heal = 5,
	Hurt = 6,
	GotBuff = 7,
	CancelBuff = 8,
	MapEffect = 9,
	Die = 10
}

public class BattleReportEvent{
	int monsterid;
	double time;
	double value1;
	double value2;
	ReportActionType actionType;

	public BattleReportEvent(int id, double t, double v1, double v2, ReportActionType type){
		monsterid = id;
		time = t;
		value1 = v1;
		value2 = v2;
		actionType = type;
	}

	public JsonData convertToJson(){
		JsonData json = new JsonData ();
		json ["id"] = monsterid;
		json ["type"] = (int)actionType;
		json ["t"] = time;
		json ["v1"] = value1;
		json ["v2"] = value2;
		return json;
	}
}

public class BattleReportGenerater
{
	private static BattleReportGenerater _instance;
	private BattleReportGenerater(){
	}
	public static BattleReportGenerater getInstance(){
		if (_instance == null) {
			_instance = new BattleReportGenerater();
		}
		return _instance;
	}

	List<BattleReportEvent> reportEventList = new List<BattleReportEvent>();

	public void clearList(){
		reportEventList.Clear ();
	}

	public void addEvent(int id, double t, double v1, double v2, ReportActionType type){
		reportEventList.Add (new BattleReportEvent (id, t, v1, v2, type));
	}

	public JsonData getReportStr(){
		JsonData[] reportArr = new JsonData[reportEventList.Count];
		for (int i=0; i<reportEventList.Count; i++) {
			reportArr[i] = reportEventList[i].convertToJson();
		}
		string json_array=JsonMapper.ToJson(reportArr);
		return json_array;
	}


}
