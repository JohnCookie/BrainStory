using System;
using System.Collections.Generic;

public class BattleMonster
{
	public float monsterRealPosX;
	public  float monsterRealPosY;
	public int monsterIndexX;
	public int mosnterIndexY;
	public int icon;
	public int hp;
	public MonsterAtkType atkType;
	public MonsterDefType defType;
	public int atk;
	public int def;
	public List<int> skills = new List<int> ();
	public List<int> talents = new List<int>();
	public int intel;
	public int dex;
	public int agi;
	public double moveSpd;
	public double atkSpd;
	public int range;
	/*
	 *     0  1  2
	 *     3  M  4
	 *     5  6  7
	 */
	public int[] freeNearPos = {0,0,0,0,0,0,0,0};

	public BattleMonster ()
	{
	}
}
