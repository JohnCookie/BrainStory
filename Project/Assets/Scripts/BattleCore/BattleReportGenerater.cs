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
	int battleResult = 0; // 0 left win 1 right win 2 error
	List<BattleUnit> leftTeamList = new List<BattleUnit> ();
	List<BattleUnit> rightTeamList = new List<BattleUnit> ();

	public void reset(){
		reportEventList.Clear ();
		leftTeamList.Clear ();
		rightTeamList.Clear ();
	}

	public void addEvent(int id, double t, double v1, double v2, ReportActionType type){
		reportEventList.Add (new BattleReportEvent (id, t, v1, v2, type));
	}

	public string getReportStr(){
//		List<JsonData> reportList = new List<JsonData>();
//		for (int i=0; i<reportEventList.Count; i++) {
//			reportList.Add(reportEventList[i].convertToJson());
//		}
//		string json_array=JsonMapper.ToJson(reportList);
//		return json_array;
		string reportArr = "[";
		for(int i=0;i<reportEventList.Count;i++){
			reportArr+=reportEventList[i].convertToJson().ToJson();
			if(i!=reportEventList.Count-1){
				reportArr+=",";
			}
		}
		reportArr+="]";
		return reportArr;
	}

	public void setLeftTeam(BattleTeam team){
		foreach (BattleMonsterBase monster in team.m_monsterList) {
			BattleUnit unit = new BattleUnit();
			unit.monster_id = monster.userMonsterId;
			unit.battle_id = monster.battleUnitId;
			leftTeamList.Add(unit);
		}
	}
	public void setRightTeam(BattleTeam team){
		foreach (BattleMonsterBase monster in team.m_monsterList) {
			BattleUnit unit = new BattleUnit();
			unit.monster_id = monster.userMonsterId;
			unit.battle_id = monster.battleUnitId;
			rightTeamList.Add(unit);
		}
	}

	public string getTeamStr(){
		return "["+JsonMapper.ToJson(leftTeamList)+","+JsonMapper.ToJson(rightTeamList)+"]";
	}

	public void setBattleResult(int result){
		battleResult = result;
	}

	public string getWholeJsonStr ()
	{
		string report = "{";
		report += "\"result\":"+battleResult+",";
		report += "\"team\":"+getTeamStr()+",";
		report += "\"reward\":{},";
		report += "\"report\":"+getReportStr()+",";
		report += "\"extra\":\"\"";
		report += "}";
		return report;
	}
}
