using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleTeam{
	public List<UserMonster> m_monsterList = new List<UserMonster>();
	/*
	 *     0  1  2
	 *     3  4  5
	 *     6  7  8
	 */
	public long[] m_monsterPlace = {0,0,0,0,0,0,0,0,0};
	public Dictionary<long, UserMonster> m_monsterDict = new Dictionary<long, UserMonster> ();
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

	public int[,] battleMapData = new int[18,9];
	public BattleTeam playerBattleMonsterTeam;
	public BattleTeam enermyBattleMosnterTeam;
}
