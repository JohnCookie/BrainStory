using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class BattleReplayMgr{
	BattleReplayInfo m_reportInfo;
	List<ReplayReport> m_reportSorted;

	public BattleReplayMgr(string report){
		Debug.Log("create replay mgr: "+report);
		m_reportInfo = JsonMapper.ToObject<BattleReplayInfo> (report);
		m_reportSorted = m_reportInfo.report;
		m_reportSorted.Sort (delegate(ReplayReport x, ReplayReport y) {
			if(x.t<y.t){
				return -1;
			}else if(x.t>y.t){
				return 1;
			}else{
				if(x.type<y.type){
					return -1;
				}else{
					return 1;
				}
			}
		});

	}

	public int getResult(){
		return m_reportInfo.result;
	}

	public List<BattleUnit> getLeftTeam(){
		return m_reportInfo.team [0];
	}

	public List<BattleUnit> getRightTeam()
	{
		return m_reportInfo.team [1];
	}

	public List<ReplayReport> getReports(){
		return m_reportSorted;
	}

	public BattleReward getReward(){
		return m_reportInfo.reward;
	}

	public string getExtraInfo(){
		return m_reportInfo.extra;
	}
}

