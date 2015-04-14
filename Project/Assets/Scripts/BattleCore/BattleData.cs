using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MapTileType {
	None = 0,
	Monster = 1,
	Occupied = 2,
	Obstacle = 3
}

// save data in battle
public class BattleData {
	private static BattleData _instance;
	private BattleData(){

	}
	public static BattleData getInstance(){
		if (_instance == null) {
			_instance = new BattleData();	
		}
		return _instance;
	}

	/*
	   xy 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7
		0 x x x x x x x x x x x x x x x x x x
	 	1 x x x x x x x x x x x x x x x x x x
		2 x 0 x 1 x 2 x x x x x x 0 x 1 x 2 x
		3 x x x x x x x x x x x x x x x x x x
		4 x 3 x 4 x 5 x x x x x x 3 x 4 x 5 x
		5 x x x x x x x x x x x x x x x x x x
		6 x 6 x 7 x 8 x x x x x x 6 x 7 x 8 x
		7 x x x x x x x x x x x x x x x x x x
		8 x x x x x x x x x x x x x x x x x x
	 */
	public int[,] battleMapData = new int[GameConfigs.map_max_x_index, GameConfigs.map_max_y_index];

	public BattleTeam playerBattleMonsterTeam = new BattleTeam();
	public BattleTeam enermyBattleMosnterTeam = new BattleTeam();

	public int battleMonsterNum = 0;

	public void reset ()
	{
		battleMapData = new int[GameConfigs.map_max_x_index, GameConfigs.map_max_y_index];
		playerBattleMonsterTeam = new BattleTeam();
		enermyBattleMosnterTeam = new BattleTeam();
		battleMonsterNum = 0;
	}

	public int getBattleMonsterId(){
		battleMonsterNum++;
		return battleMonsterNum;
	}
}
