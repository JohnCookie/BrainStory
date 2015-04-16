using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleMapUtil
{
	static Vector2[] range_one_near_offset = {new Vector2 (-1, 0), new Vector2 (0, -1), new Vector2 (1, 0), new Vector2 (0, 1)};
	static Vector2[] range_two_near_offset = {new Vector2 (-2, 0), new Vector2 (-1, -1), new Vector2 (0, -2), new Vector2 (1, -1), 
										new Vector2 (2, 0), new Vector2 (1, 1), new Vector2 (0, 2), new Vector2 (-1, 1)};
	static Vector2[] range_three_near_offset = {new Vector2 (-3, 0), new Vector2 (-2, -1), new Vector2 (-1, -2), new Vector2 (0, -3), 
										new Vector2 (1, -2), new Vector2 (2, -1), new Vector2 (3, 0), new Vector2 (2, 1),
										new Vector2 (1, 2), new Vector2 (0, 3), new Vector2 (-1, 2), new Vector2 (-2, 1)};
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

	public static Vector2 getNextStepPosition(BattleMonster _m){

		BattleMonster targetMonster = null;
		BattleTeam tarTeam = null;
		if (_m.team == TeamType.LeftTeam) {
			tarTeam = BattleData.getInstance().enermyBattleMosnterTeam;
		}
		if (_m.team == TeamType.RightTeam) {
			tarTeam = BattleData.getInstance().playerBattleMonsterTeam;
		}
		int distance = 999;
		foreach(BattleMonster bm in tarTeam.m_monsterList){
			if(getDistanceBetween2Monster(bm,_m)<distance){
				distance=getDistanceBetween2Monster(bm,_m);
				targetMonster=bm;
			}
		}

		Vector2 targetPosition = getTarPosition (_m, targetMonster);
		if (_m.monsterIndexX != targetPosition.x || _m.monsterIndexY != targetPosition.y) {
			_m.targetMonster = targetMonster;
			Debug.Log("Monster_"+_m.battleUnitId+" aimed on Monster_"+targetMonster.battleUnitId);	
		}
		return targetPosition;
	}

	public static Vector2 PositionInVector(double x, double y){
		Vector2 result = new Vector2 ();
		result.x = (int)(x / GameConfigs.map_grid_width);
		result.y = (int)(-y / GameConfigs.map_grid_width);

		return result;
	}

	public static int getDistanceBetween2Monster(BattleMonster m1, BattleMonster m2){
		return Math.Abs(m1.monsterTargetIndexX-m2.monsterIndexX)+Math.Abs(m1.monsterTargetIndexY-m2.monsterIndexY);
	}

	static int getDistanceBetween2Vector2(Vector2 v1, Vector2 v2){
		return (int)(Math.Abs(v1.x-v2.x)+Math.Abs(v1.y-v2.y));
	}

	static Vector2 getTarPosition(BattleMonster startMonster, BattleMonster targetMonster){
		// find free nearTargetMonster
		List<Vector2> freePositionList = new List<Vector2> ();
		foreach(Vector2 posOffsets in getOffsetsByRange(startMonster.range)){
			if((targetMonster.monsterTargetIndexX+(int)posOffsets.x>=0) && (targetMonster.monsterTargetIndexY+(int)posOffsets.y>=0)){
				if(IsTileFree(targetMonster.monsterTargetIndexX+(int)posOffsets.x,targetMonster.monsterTargetIndexY+(int)posOffsets.y)){
					freePositionList.Add(new Vector2(targetMonster.monsterTargetIndexX+posOffsets.x,targetMonster.monsterTargetIndexY+posOffsets.y));
				}
			}
		}

		// find nearlist point in list
		int distance = 999;
		Vector2 targetPos = new Vector2 ();
		foreach (Vector2 v2 in freePositionList) {
			if(getDistanceBetween2Vector2(new Vector2(startMonster.monsterIndexX,startMonster.monsterIndexY),v2)<distance){
				distance=getDistanceBetween2Vector2(new Vector2(startMonster.monsterIndexX,startMonster.monsterIndexY),v2);
				targetPos.x = v2.x;
				targetPos.y = v2.y;
			}	
		}
		if (targetPos.x == 0 && targetPos.y == 0) {
			targetPos.x = startMonster.monsterIndexX;
			targetPos.y = startMonster.monsterIndexY;
		}

		Vector2 moveToward = new Vector2 ();
		// find the position move towards which near self position
		if (targetPos.x > startMonster.monsterIndexX) {
			moveToward.x = startMonster.monsterIndexX + 1;
		} else if (targetPos.x < startMonster.monsterIndexX) {
			moveToward.x = startMonster.monsterIndexX - 1;
		} else {
			moveToward.x = startMonster.monsterIndexX;
		}

		if (targetPos.y > startMonster.monsterIndexY) {
			moveToward.y = startMonster.monsterIndexY + 1;
		} else if (targetPos.y < startMonster.monsterIndexY) {
			moveToward.y = startMonster.monsterIndexY - 1;
		} else {
			moveToward.y = startMonster.monsterIndexY;
		}

		return moveToward;
	}

	static bool IsTileFree(int _x, int _y){
		return BattleData.getInstance().battleMapData[_x,_y]==(int)MapTileType.None;
	}

	static Vector2[] getOffsetsByRange(int range){
		switch (range) {
		case 1:
			return range_one_near_offset;
		case 2:
			return range_two_near_offset;
		case 3:
			return range_three_near_offset;
		default:
			return range_one_near_offset;
		}
	}

}

