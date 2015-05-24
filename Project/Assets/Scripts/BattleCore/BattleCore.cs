using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleCore
{
	public void StartBattle(Dictionary<int, UserMonster> playerMosnterTeam, Dictionary<int, UserMonster> enermyMonsterTeam){
		InitBattleData ();
		InitMap ();

		BattleReportGenerater.getInstance ().reset ();

		InitPlayerBattleMonster (playerMosnterTeam);
		InitEnermyBattleMonster (enermyMonsterTeam);

		BattleStart ();

		PrintBattleReport ();
	}

	void InitMap(){
		for (int x=0; x<GameConfigs.map_max_x_index; x++) {
			for(int y=0; y<GameConfigs.map_max_y_index; y++){
				BattleData.getInstance().battleMapData[x,y] = (int)MapTileType.None;
			}
		}
	}

	void InitBattleData(){
		BattleData.getInstance ().reset ();
	}

	void InitPlayerBattleMonster(Dictionary<int, UserMonster> _team){
		// player monsters
		BattleData.getInstance ().playerBattleMonsterTeam.reset ();
		foreach (int key in _team.Keys) {
			BattleMonsterBase _monster = new TestConcreteMonster(_team[key], BattleMapUtil.getMapIndexOnInit(key, TeamType.LeftTeam), TeamType.LeftTeam);
			BattleData.getInstance().playerBattleMonsterTeam.addOneMonster(_monster);
		}
		BattleReportGenerater.getInstance ().setLeftTeam (BattleData.getInstance().playerBattleMonsterTeam);
	}

	void InitEnermyBattleMonster(Dictionary<int, UserMonster> _team){
		// enermy monsters
		BattleData.getInstance ().enermyBattleMosnterTeam.reset ();
		foreach (int key in _team.Keys) {
			BattleMonsterBase _monster = new TestConcreteMonster(_team[key], BattleMapUtil.getMapIndexOnInit(key, TeamType.RightTeam), TeamType.RightTeam);
			BattleData.getInstance().enermyBattleMosnterTeam.addOneMonster(_monster);
		}
		BattleReportGenerater.getInstance ().setRightTeam (BattleData.getInstance ().enermyBattleMosnterTeam);
	}

	void BattleStart(){
		Debug.Log("----- Battle Start -----");
		// Init monsters on map
		foreach (BattleMonsterBase m in BattleData.getInstance().playerBattleMonsterTeam.m_monsterList) {
			BattleData.getInstance().battleMapData[m.monsterIndexX, m.monsterIndexY] = (int)MapTileType.Monster;
			m.Born();
		}
		foreach (BattleMonsterBase m in BattleData.getInstance().enermyBattleMosnterTeam.m_monsterList) {
			BattleData.getInstance().battleMapData[m.monsterIndexX, m.monsterIndexY] = (int)MapTileType.Monster;
			m.Born();
		}

		// Calculate init addition
		CalculateAddition ();

		// Time Tick
		for (double t=0; ; t+=GameConfigs.battle_tick_step) {
			BattleData.getInstance().currBattleTime = t;
			// check if battle end
			if(BattleData.getInstance().playerBattleMonsterTeam.getMonsterNum()<=0){
				Debug.Log("----- Battle End, Enermy(RightSide) Team Win -----");
				BattleReportGenerater.getInstance().setBattleResult(1);
				break;
			}
			if(BattleData.getInstance().enermyBattleMosnterTeam.getMonsterNum()<=0){
				Debug.Log("----- Battle End, Player(RightSide) Team Win -----");
				BattleReportGenerater.getInstance().setBattleResult(0);
				break;
			}
			// update position
			CalculateMonsterPositions();
			// update action
			MonsterActions();
			// calculate hot/dot
			CalculateBuffAndDebuffs();
			// calculate effect on map
			CalculateEffectsOnMap();

			// error happen battle not end
			if(t>60*5){
				Debug.Log("Battle time beyond 8 minutes, check what happened in log please");
				BattleReportGenerater.getInstance().setBattleResult(2);
				break;
			}
		}
	}

	void PrintBattleReport(){
		Debug.Log (BattleReportGenerater.getInstance ().getTeamStr ());
		Debug.Log(BattleReportGenerater.getInstance().getReportStr());
		Debug.Log (BattleReportGenerater.getInstance ().getWholeJsonStr ());
	}

	void CalculateAddition(){
		Debug.Log("-----> Calculate Additions");
//		foreach (BattleMonster m in BattleData.getInstance().playerBattleMonsterTeam.m_monsterList) {
//			m.calculateAddition();
//		}
//		foreach (BattleMonster m in BattleData.getInstance().enermyBattleMosnterTeam.m_monsterList) {
//			m.calculateAddition();	
//		}
	}

	void CalculateMonsterPositions(){
		foreach(BattleMonsterBase m in BattleData.getInstance().playerBattleMonsterTeam.m_monsterList){
			m.Move();
		}
		foreach(BattleMonsterBase m in BattleData.getInstance().enermyBattleMosnterTeam.m_monsterList){
			m.Move();
		}
	}

	void MonsterActions(){
		for(int i=0; i<BattleData.getInstance().playerBattleMonsterTeam.m_monsterList.Count; i++){
			BattleData.getInstance().playerBattleMonsterTeam.m_monsterList[i].updateSelfAction();
		}
		for(int j=0; j<BattleData.getInstance().enermyBattleMosnterTeam.m_monsterList.Count; j++){
			BattleData.getInstance().enermyBattleMosnterTeam.m_monsterList[j].updateSelfAction();
		}
	}

	void CalculateBuffAndDebuffs(){
//		foreach(BattleMonsterBase m in BattleData.getInstance().playerBattleMonsterTeam.m_monsterList){
//			m.updateDotDebuff();
//			m.updateHotBuff();
//		}
//		foreach(BattleMonsterBase m in BattleData.getInstance().enermyBattleMosnterTeam.m_monsterList){
//			m.updateDotDebuff();
//			m.updateHotBuff();
//		}
	}

	void CalculateEffectsOnMap(){
	}
}

