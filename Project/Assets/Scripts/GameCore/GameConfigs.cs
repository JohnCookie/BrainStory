using UnityEngine;
using System.Collections;

public class GameConfigs
{
	// key for talent extend rate in summon
	public static string SACRIFICE_SUMMON_TALENT_EXTEND_KEY = "4";
	// summon speed scale
	public static float summon_speed_scale = 1.0f;
	// battle map
	public static int map_max_x_index = 18;
	public static int map_max_y_index = 9;
	public static double map_grid_width = 40.0;
	// battle setting
	public static double battle_tick_step = 0.025;

	// monster intelligence for cast percent
	public static double intel_per_cast = 0.06;
	public static double cast_max_percent = 0.6;
	// monster agi for crit
	public static double agi_per_crit = 0.06;
	public static double crit_max_percent = 0.6;
	// monster dex for dodge
	public static double dex_per_dodge = 0.06;
	public static double dodge_max_percent = 0.6;
}
