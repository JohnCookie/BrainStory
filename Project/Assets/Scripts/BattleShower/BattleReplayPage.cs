using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleReplayPage : MonoBehaviour
{
	UIProgressBar m_progressLeft;
	UIProgressBar m_progressRight;
	Vector3 mapOffset;
	BattleReplayMgr repMgr;
	double currTime = 0;
	int reportLength = 0;
	bool replayStart = false;
	Dictionary<int, BaseMonsterShower> m_monsterDict = new Dictionary<int, BaseMonsterShower>();
	GameObject factoryObj;
	Queue<ReplayReport> m_queueReport = new Queue<ReplayReport> ();
	GameObject m_battleMap;

	void Awake(){
		m_progressLeft = transform.FindChild ("TeamInfoPart").FindChild ("LeftTeam").FindChild ("HpProgressBar").GetComponent<UIProgressBar> ();
		m_progressRight = transform.FindChild ("TeamInfoPart").FindChild ("RightTeam").FindChild ("HpProgressBar").GetComponent<UIProgressBar> ();
		m_battleMap = transform.FindChild ("BattleMapPart").FindChild ("MapNode").gameObject;
		InitFactory ();
	}

	void Start(){
		// init map and views

		// start play battle
		TimerHelper.getInstance ().DelayFunc (3, startPlayReport);
	}

	void playReport(){
		// init reports
		string reportStr = PlayerPrefs.GetString ("battle_report");
		if (string.IsNullOrEmpty (reportStr)) {
			UISystem.getInstance().showCommonDialog(CommonDialogStyle.OnlyConfirmStyle, "Error", "Battle report error(empty)", null, null, delegate(string msg) {
				closeBattlePage(msg);
			});
			return;
		}
		repMgr = new BattleReplayMgr (reportStr);
		
		// init monsters
		List<BattleUnit> leftTeam = repMgr.getLeftTeam ();
		foreach (BattleUnit bu in leftTeam) {
			BaseMonsterShower temp = BattleMonsterPrefabFactory.getInstance().createMonsterShower(UserDataGenerater.GetInstance().getUserMonsterByUniqueId(bu.monster_id).monster_id);
			temp.transform.parent = m_battleMap.transform;
			temp.transform.localScale = Vector3.one;
			m_monsterDict.Add(bu.battle_id,  temp);
		}
		List<BattleUnit> rightTeam = repMgr.getRightTeam ();
		foreach (BattleUnit bu in rightTeam) {	
			BaseMonsterShower temp = BattleMonsterPrefabFactory.getInstance().createMonsterShower(UserDataGenerater.GetInstance().getUserMonsterByUniqueId(bu.monster_id).monster_id);
			temp.transform.parent = m_battleMap.transform;
			temp.transform.localScale = Vector3.one;
			m_monsterDict.Add(bu.battle_id,  temp);
		}
		
		// init report queue
		List<ReplayReport> sortReplays = repMgr.getReports ();
		for (int i=0; i<sortReplays.Count; i++) {
			m_queueReport.Enqueue(sortReplays[i]);
		}
		
		replayStart = true;
	}

	void OnDestroy(){
		Destroy (factoryObj);
	}

	void Update(){
		if (replayStart) {
			ReplayReport tempReport;
			tempReport = m_queueReport.Peek();
			while(tempReport.time<=currTime){
				tempReport = m_queueReport.Dequeue();
				handleReplay(tempReport);
				tempReport = m_queueReport.Peek();
			}
			currTime+=Time.deltaTime;
			if(m_queueReport.Count<=0){
				replayStart = false;
				Debug.Log("Replay show end");
				int result = repMgr.getResult();
				switch(result){
				case 0:
					UISystem.getInstance().showCommonDialog(CommonDialogStyle.OnlyConfirmStyle, "Battle Result", "You Win!", null, null, closeBattlePage);
					break;
				case 1:
					UISystem.getInstance().showCommonDialog(CommonDialogStyle.OnlyConfirmStyle, "Battle Result", "You Lose!", null, null, closeBattlePage);
					break;
				case 2:
					UISystem.getInstance().showCommonDialog(CommonDialogStyle.OnlyConfirmStyle, "Battle Result", "Battle Error, Please check report in log", null, null, closeBattlePage);
					break;
				}
			}
		}
	}

	void handleReplay(ReplayReport report){
		BaseMonsterShower bm = m_monsterDict [report.id];
		switch ((ReportActionType)report.type) {
		case ReportActionType.Locate:
			bm.Locate(report.v1, report.v2);
			break;
		case ReportActionType.Move:
			bm.Move(report.v1, report.v2, report.v3);
			break;
		case ReportActionType.Attack:
			bm.Attack();
			break;
		case ReportActionType.Cast:
			bm.Cast(report.v1);
			break;
		case ReportActionType.Hurt:
			bm.Hurted(report.v1);
			break;
		case ReportActionType.Heal:
			bm.Healed(report.v1);
			break;
		case ReportActionType.Die:
			bm.Die();
			break;
		default:
			break;
		}
	}

	void InitFactory(){
		ResourceSystem.getInstance().loadRes("Prefabs/Battle/BattleMonsterFactory", (obj)=>{
			GameObject factory = obj as GameObject;
			factoryObj = (GameObject)Instantiate (factory);
			factoryObj.name = "BattleMonsterFactory";
			factoryObj.transform.localPosition = Vector3.zero;
			factoryObj.transform.localScale = Vector3.one;
			factoryObj.transform.parent = transform;
		});
	}

	void closeBattlePage(string msg){
		UISystem.getInstance ().showLastPage ();
	}

	public void startPlayReport(){
		playReport ();
	}
}

