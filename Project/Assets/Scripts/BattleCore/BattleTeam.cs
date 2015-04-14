using System;
using System.Collections.Generic;

public class BattleTeam{
	int extra = 0;
	public List<BattleMonster> m_monsterList;

	public BattleTeam(){
		extra = 0;
		m_monsterList = new List<BattleMonster> ();
		m_monsterList.Clear ();
	}

	public void reset ()
	{
		extra = 0;
		m_monsterList = new List<BattleMonster> ();
		m_monsterList.Clear ();
	}

	public void addOneMonster(BattleMonster _monster){
		m_monsterList.Add (_monster);
	}

	public void removeOneMonster(BattleMonster _monster){
		m_monsterList.Remove (_monster);
	}

	public int monsterNum(){
		return m_monsterList.Count;
	}
}
