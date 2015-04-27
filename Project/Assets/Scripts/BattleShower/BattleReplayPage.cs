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
	Queue<ReplayReport> m_queueReport;
	GameObject m_battleMap;

	void Awake(){
		InitFactory ();
	}

	void Start(){
		// init map and views

		// init reports
		string reportStr = PlayerPrefs.GetString ("battle_report");
		if (string.IsNullOrEmpty (reportStr)) {
			UISystem.getInstance().showCommonDialog(CommonDialogStyle.OnlyConfirmStyle, "Error", "Battle report error", null, null, delegate(string msg) {
				closeBattlePage();
			});
			return;
		}
		BattleReplayMgr replayMgr = new BattleReplayMgr (reportStr);

		// init monsters
		List<BattleUnit> leftTeam = replayMgr.getLeftTeam ();
		foreach (BattleUnit bu in leftTeam) {
			BaseMonsterShower temp = BattleMonsterPrefabFactory.getInstance().createMonsterShower(UserDataGenerater.GetInstance().getUserMonsterByUniqueId(bu.monster_id).monster_id);
			temp.transform.parent = m_battleMap.transform;
			m_monsterDict.Add(bu.battle_id,  temp);
		}
		List<BattleUnit> rightTeam = replayMgr.getRightTeam ();
		foreach (BattleUnit bu in rightTeam) {	
			BaseMonsterShower temp = BattleMonsterPrefabFactory.getInstance().createMonsterShower(UserDataGenerater.GetInstance().getUserMonsterByUniqueId(bu.monster_id).monster_id);
			temp.transform.parent = m_battleMap.transform;
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
			while(m_queueReport.Peek().time<=currTime){
				handleReplay(m_queueReport.Dequeue());
			}
			currTime+=Time.deltaTime;
			if(m_queueReport.Count<=0){
				replayStart = false;
				Debug.Log("Replay show end");
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
		});
	}

	void closeBattlePage(){
		UISystem.getInstance ().showLastPage ();
	}
}

