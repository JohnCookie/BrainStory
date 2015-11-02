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

#region NormalTask
public class NormalTaskList{
	public List<NormalTaskData> data { get; set; }
}

public class NormalTaskData
{
	public int id { get; set; }
	public string task_name { get; set; }
	public int task_type { get; set; }
	public int task_duration { get; set; }
	public List<TaskOpponentData> task_opp { get; set; }
	public List<TaskRewardData> task_reward { get; set; }
	public int task_exp { get; set; }
	public int task_gold { get; set; }
	public int task_max_num { get; set; }
}

public class TaskOpponentData
{
	public int monsterId { get; set; }
	public int pos { get; set; }
	public List<int> talents { get; set; }
	public List<int> skills { get; set; }
	public int lv { get; set; }
}

public class TaskRewardData
{
	public int itemId { get; set; }
	public int rate { get; set; }
}
#endregion

#region SpecTask
public class SpecTaskList{
	public List<SpecTaskData> data { get; set; }
}

public class SpecTaskData
{
	public int id { get; set; }
	public string task_name { get; set; }
	public int task_type { get; set; }
	public int task_duration { get; set; }
	public List<TaskOpponentData> task_opp { get; set; }
	public List<TaskRewardData> task_reward { get; set; }
	public int task_exp { get; set; }
	public int task_gold { get; set; }
	public int task_max_num { get; set; }
	public List<int> task_day { get; set; }
	public List<int> task_time { get; set; }
	public List<int> task_require { get; set; }
	public int task_rate { get; set; }
}
#endregion

#region TaskData
public class AllTaskData{
	public List<TaskData> data { get; set; }
}

public class TaskData
{
	public int id { get; set; }
	public string task_name { get; set; }
	public int duration { get; set; } // minute
	public string task_content { get; set; }
	public List<TaskRewardData> task_reward { get; set; }
	public int task_rarity { get; set; }
}
#endregion

#region UserData Task
public class UserTaskInfo
{
	public int id { get; set; }
	public List<long> usedMonster { get; set; }
	public long startTime { get; set; }
}

public class UserTasksList
{
	public List<UserTaskInfo> data { get; set; }
}

public class UserTaskBaseInfo
{
	public int id { get; set; }
	public long startTime { get; set; }
}

public class UserTaskBaseList
{
	public List<UserTaskBaseInfo> data { get; set; }
}
#endregion