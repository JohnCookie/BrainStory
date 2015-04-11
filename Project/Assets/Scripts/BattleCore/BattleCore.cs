using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleCore
{
	public void StartBattle(BattleTeam playerMosnterTeam, BattleTeam enermyMonsterTeam){
		InitMap ();

		BattleData.getInstance ().playerBattleMonsterTeam = playerMosnterTeam;
		BattleData.getInstance ().enermyBattleMosnterTeam = enermyMonsterTeam;
		InitBattleMonster ();
	}

	void InitMap(){
		for (int x=0; x<GameConfigs.map_max_x_index; x++) {
			for(int y=0; y<GameConfigs.map_max_y_index; y++){
				BattleData.getInstance().battleMapData[x,y] = 0;
			}
		}
	}

	void InitBattleMonster(){
		// player monsters
		// generate dictionary
		BattleData.getInstance ().playerBattleMonsterTeam.m_monsterDict.Clear ();
		for (int i=0; i<BattleData.getInstance().playerBattleMonsterTeam.m_monsterList.Count; i++) {
			BattleData.getInstance ().playerBattleMonsterTeam.m_monsterDict.Add(BattleData.getInstance().playerBattleMonsterTeam.m_monsterList[i].id,
			                                                                    BattleData.getInstance().playerBattleMonsterTeam.m_monsterList[i]);
		}

	}
}

