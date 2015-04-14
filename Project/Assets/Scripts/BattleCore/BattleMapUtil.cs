using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleMapUtil
{
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
	public static Vector2 getMapIndexOnInit(int p_index, TeamType _team){
		int row_index = (int)(p_index/3);
		int col_index = p_index%3;
		int _x = 0;
		int _y = 0;
		if (_team == TeamType.LeftTeam) {
			_x = 1+col_index*2;
			_y = 2+row_index*2;
		} else {
			_x = (GameConfigs.map_max_x_index-6) + col_index*2;
			_y = 2+row_index*2;
		}
		return new Vector2 (_x, _y);
	}
}

