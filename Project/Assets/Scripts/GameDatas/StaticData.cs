using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#region for test
public class PlayerRecord
{
	public int id { get; set; }
	public int gold { get; set; }
	public int time { get; set; }
}

public class PlayerUnlock
{
	public string stage1 { get; set; }
	public string stage2 { get; set; }
	public string stage3 { get; set; }
}

public class TestJsonData
{
	public string player_name { get; set; }
	public List<PlayerRecord> player_records { get; set; }
	public PlayerUnlock player_unlock { get; set; }
}
#endregion

#region monster_base
public class MonsterBaseList{
	public List<MonsterBase> data { get; set; }
}

public class MonsterBase
{
	public int id { get; set; }
	public string name { get; set; }
	public int icon { get; set; }
	public int quality { get; set; }
	public int atk { get; set; }
	public int def { get; set; }
	public double atk_spd { get; set; }
	public double mov_spd { get; set; }
	public int range { get; set; }
	public int intel { get; set; }
	public int agi { get; set; }
	public int dex { get; set; }
	public int hp { get; set; }
	public int atk_type { get; set; }
	public int def_type { get; set; }
	public double atk_add { get; set; }
	public double def_add { get; set; }
	public double spd_add { get; set; }
	public double int_add { get; set; }
	public double agi_add { get; set; }
	public double dex_add { get; set; }
	public double hp_add { get; set; }
}
#endregion

#region UserData monster
public class UserMonster
{
	public long id {get; set;}
	public int monster_id {get; set;}
	public int exp {get; set;}
	public List<int> skills {get; set;}
	public List<int> talents {get; set;}
}
#endregion

#region BattleReplayReport
public class BattleReward
{
	public int exp { get; set; }
	public int gold { get; set; }
}

public class ReplayReport
{
	public int type { get; set; }
	public int id { get; set; }
	public double t { get; set; }
	public double v1 { get; set; }
	public double v2 { get; set; }
	public double v3 { get; set; }
}

public class BattleUnit
{
	public long monster_id { get; set; }
	public int battle_id { get; set; }
}

public class BattleReplayInfo
{
	public int result { get; set; }
	public List<List<BattleUnit>> team { get; set; }
	public BattleReward reward { get; set; }
	public List<ReplayReport> report { get; set; }
	public string extra { get; set; }
}
#endregion
