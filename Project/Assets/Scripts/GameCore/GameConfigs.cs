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
	public static double battle_tick_step = 0.1;

	// monster intelligence for cast percent
	public static double intel_to_cast = 0.6;
}
