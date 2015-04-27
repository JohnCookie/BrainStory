using System;
using UnityEngine;

public class BattleMonsterFactory
{
	private static BattleMonsterFactory _instance;
	private BattleMonsterFactory (){
	}
	public static BattleMonsterFactory getInstance(){
		if (_instance == null) {
			_instance = new BattleMonsterFactory();	
		}
		return _instance;
	}

	public BattleMonsterBase createMonster(int monster_id, UserMonster _monster, Vector2 _pos, TeamType _team){
		switch (monster_id) {
		default:
			return new TestConcreteMonster(_monster, _pos, _team);
		}
	}
}
