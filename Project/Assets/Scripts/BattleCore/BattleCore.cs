using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleCore
{
	public void StartBattle(Dictionary<int, UserMonster> playerMosnterTeam, Dictionary<int, UserMonster> enermyMonsterTeam){
		InitBattleData ();
		InitMap ();
		InitPlayerBattleMonster (playerMosnterTeam);
		InitEnermyBattleMonster (enermyMonsterTeam);

		BattleStart ();
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
			TestMonster _monster = new TestMonster(_team[key], BattleMapUtil.getMapIndexOnInit(key, TeamType.LeftTeam), TeamType.LeftTeam);
			BattleData.getInstance().playerBattleMonsterTeam.addOneMonster(_monster);
		}
	}

	void InitEnermyBattleMonster(Dictionary<int, UserMonster> _team){
		// enermy monsters
		BattleData.getInstance ().enermyBattleMosnterTeam.reset ();
		foreach (int key in _team.Keys) {
			TestMonster _monster = new TestMonster(_team[key], BattleMapUtil.getMapIndexOnInit(key, TeamType.RightTeam), TeamType.RightTeam);
			BattleData.getInstance().enermyBattleMosnterTeam.addOneMonster(_monster);
		}
	}

	void BattleStart(){
		Debug.Log("----- Battle Start -----");
		// Init monsters on map
		foreach (BattleMonster m in BattleData.getInstance().playerBattleMonsterTeam.m_monsterList) {
			BattleData.getInstance().battleMapData[m.monsterIndexX, m.monsterIndexY] = (int)MapTileType.Monster;
		}
		foreach (BattleMonster m in BattleData.getInstance().enermyBattleMosnterTeam.m_monsterList) {
			BattleData.getInstance().battleMapData[m.monsterIndexX, m.monsterIndexY] = (int)MapTileType.Monster;	
		}

		// Calculate init addition
		CalculateAddition ();

		// Time Tick
		for (double t=0;; t+=GameConfigs.battle_tick_step) {
			// check if battle end
			if(BattleData.getInstance().playerBattleMonsterTeam.getMonsterNum()<=0){
				Debug.Log("----- Battle End, Enermy(RightSide) Team Win -----");
				break;
			}
			if(BattleData.getInstance().enermyBattleMosnterTeam.getMonsterNum()<=0){
				Debug.Log("----- Battle End, Player(RightSide) Team Win -----");
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
		}
	}

	void CalculateAddition(){
		Debug.Log("-----> Calculate Additions");
		foreach (BattleMonster m in BattleData.getInstance().playerBattleMonsterTeam.m_monsterList) {
			m.calculateAddition();
		}
		foreach (BattleMonster m in BattleData.getInstance().enermyBattleMosnterTeam.m_monsterList) {
			m.calculateAddition();	
		}
	}

	void CalculateMonsterPositions(){
		foreach(BattleMonster m in BattleData.getInstance().playerBattleMonsterTeam.m_monsterList){
			m.updatePosition();
		}
		foreach(BattleMonster m in BattleData.getInstance().enermyBattleMosnterTeam.m_monsterList){
			m.updatePosition();
		}
	}

	void MonsterActions(){
		foreach(BattleMonster m in BattleData.getInstance().playerBattleMonsterTeam.m_monsterList){
			m.updateSelfAction();
		}
		foreach(BattleMonster m in BattleData.getInstance().enermyBattleMosnterTeam.m_monsterList){
			m.updateSelfAction();
		}
	}

	void CalculateBuffAndDebuffs(){
		foreach(BattleMonster m in BattleData.getInstance().playerBattleMonsterTeam.m_monsterList){
			m.updateDotDebuff();
			m.updateHotBuff();
		}
		foreach(BattleMonster m in BattleData.getInstance().enermyBattleMosnterTeam.m_monsterList){
			m.updateDotDebuff();
			m.updateHotBuff();
		}
	}

	void CalculateEffectsOnMap(){
	}
}

