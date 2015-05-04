using System;
using System.Collections.Generic;

public class BattleSimulateTest
{
	public BattleSimulateTest(){

	}

	public void simulateBattle(){
		BattleCore b_core = new BattleCore ();
		Dictionary<int, UserMonster> leftTeam = new Dictionary<int, UserMonster> ();
		Dictionary<int, UserMonster> rightTeam = new Dictionary<int, UserMonster> ();

		UserDataGenerater.GetInstance ().AddNewMonsterById (1);
		UserDataGenerater.GetInstance ().AddNewMonsterById (2);
		//UserDataGenerater.GetInstance ().AddNewMonsterById (1);
		UserMonster monster_1 = UserDataGenerater.GetInstance().UserMonsterDataList[0];
		UserMonster monster_2 = UserDataGenerater.GetInstance ().UserMonsterDataList[1];
		//UserMonster monster_3 = UserDataGenerater.GetInstance ().UserMonsterDataList [2];
		leftTeam.Add (4, monster_1);
		//leftTeam.Add (6, monster_3);
		rightTeam.Add (4, monster_2);
		b_core.StartBattle (leftTeam, rightTeam);
	}
}

