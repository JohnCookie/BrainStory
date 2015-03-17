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
	public List<MonsterBase> monsterBaseList { get; set; }
}

public class MonsterBase
{
	public int id { get; set; }
	public string name { get; set; }
	public int icon { get; set; }
	public int quality { get; set; }
	public int atk { get; set; }
	public int def { get; set; }
	public double spd { get; set; }
	public int intel { get; set; }
	public int agi { get; set; }
	public int dex { get; set; }
	public int hp { get; set; }
	public int atk_type { get; set; }
	public int def_type { get; set; }
	public int atk_add { get; set; }
	public double def_add { get; set; }
	public double spd_add { get; set; }
	public double int_add { get; set; }
	public double agi_add { get; set; }
	public double dex_add { get; set; }
	public int hp_add { get; set; }
}
#endregion