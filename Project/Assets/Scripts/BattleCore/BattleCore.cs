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
			BattleMonster _monster = new BattleMonster(_team[key], BattleMapUtil.getMapIndexOnInit(key, TeamType.LeftTeam), TeamType.LeftTeam);
			BattleData.getInstance().playerBattleMonsterTeam.addOneMonster(_monster);
		}
	}

	void InitEnermyBattleMonster(Dictionary<int, UserMonster> _team){
		// enermy monsters
		BattleData.getInstance ().enermyBattleMosnterTeam.reset ();
		foreach (int key in _team.Keys) {
			BattleMonster _monster = new BattleMonster(_team[key], BattleMapUtil.getMapIndexOnInit(key, TeamType.RightTeam), TeamType.RightTeam);
			BattleData.getInstance().enermyBattleMosnterTeam.addOneMonster(_monster);
		}
	}

	void BattleStart(){
		// Init monsters on map
		foreach (BattleMonster m in BattleData.getInstance().playerBattleMonsterTeam.m_monsterList) {
			BattleData.getInstance().battleMapData[m.monsterIndexX, m.monsterIndexY] = (int)MapTileType.Monster;
		}

	}
}

