using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

