using System;
using System.Collections.Generic;

public class BattleTeam{
	int extra = 0;
	public List<BattleMonsterBase> m_monsterList;

	public BattleTeam(){
		reset ();
	}

	public void reset ()
	{
		extra = 0;
		m_monsterList = new List<BattleMonsterBase> ();
		m_monsterList.Clear ();
	}

	public void addOneMonster(BattleMonsterBase _monster){
		m_monsterList.Add (_monster);
		BattleData.getInstance ().battleMonsterBaseDict.Add (_monster.battleUnitId, _monster);
	}

	public void removeOneMonster(BattleMonsterBase _monster){
		m_monsterList.Remove (_monster);
		BattleData.getInstance ().battleMonsterBaseDict.Remove (_monster.battleUnitId);
	} 

	public int getMonsterNum(){
		return m_monsterList.Count;
	}
}
