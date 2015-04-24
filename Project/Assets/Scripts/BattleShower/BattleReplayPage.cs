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

		}
	}

	void Update(){
		if (replayStart) {
				
		}
	}

	void InitFactory(){
		ResourceSystem.getInstance().loadRes("Prefabs/Battle/BattleMonsterFactory", (obj)=>{
			GameObject factory = obj as GameObject;
			GameObject factoryObj = (GameObject)Instantiate (factory);
			factoryObj.name = "BattleMonsterFactory";
			factoryObj.transform.localPosition = Vector3.zero;
			factoryObj.transform.localScale = Vector3.one;
		});
	}

	void closeBattlePage(){
		UISystem.getInstance ().showLastPage ();
	}
}

