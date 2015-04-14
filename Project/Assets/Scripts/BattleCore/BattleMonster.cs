using System;
using System.Collections.Generic;
using UnityEngine;

public enum TeamType{
	RightTeam,
	LeftTeam
}

public class BattleMonster
{
	public double monsterRealPosX;
	public double monsterRealPosY;
	public int monsterIndexX;
	public int monsterIndexY;
	public int icon;
	public double hp;
	public MonsterAtkType atkType;
	public MonsterDefType defType;
	public double atk;
	public double def;
	public List<int> skills = new List<int> ();
	public List<int> talents = new List<int>();
	public double intel;
	public double dex;
	public double agi;
	public double moveSpd;
	public double atkSpd;
	public int range;
	public TeamType team; // 1 right 2 left
	/*
	 *     0  1  2
	 *     3  M  4
	 *     5  6  7
	 */

	public BattleMonster (UserMonster _monster, Vector2 _pos, TeamType _team){
		// pos info
		monsterIndexX = (int)_pos.x;
		monsterIndexY = (int)_pos.y;
		monsterRealPosX = _pos.x * GameConfigs.map_grid_width - GameConfigs.map_grid_width*0.5;
		monsterRealPosY = -_pos.y * GameConfigs.map_grid_width + GameConfigs.map_grid_width * 0.5;

		MonsterBase _baseInfo = MonsterDataUntility.getInstance ().getMonsterBaseInfoById (_monster.monster_id);
		icon = _baseInfo.icon;

		int monsterLv = LvExpDataUtility.getInstance ().getMonsterLv (_monster);
		hp = _baseInfo.hp + monsterLv * _baseInfo.hp_add;
		atkType = (MonsterAtkType)_baseInfo.atk_type;
		defType = (MonsterDefType)_baseInfo.def_type;
		atk = _baseInfo.atk + monsterLv * _baseInfo.atk_add;
		def = _baseInfo.def + monsterLv * _baseInfo.def_add;
		skills.Clear ();
		foreach(int skillId in _monster.skills){
			skills.Add(skillId);
		}
		talents.Clear ();
		foreach (int talentId in _monster.talents) {
			talents.Add(talentId);	
		}
		intel = _baseInfo.intel + monsterLv * _baseInfo.int_add;
		dex = _baseInfo.dex + monsterLv * _baseInfo.dex_add;
		agi = _baseInfo.agi + monsterLv * _baseInfo.agi_add;
		moveSpd = (GameConfigs.map_grid_width / _baseInfo.mov_spd);
		atkSpd = _baseInfo.atk_spd;
		range = _baseInfo.range;
		team = _team;
	}
}
