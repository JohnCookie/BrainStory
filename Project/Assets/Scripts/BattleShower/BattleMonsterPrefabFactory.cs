using UnityEngine;
using System.Collections;

public class BattleMonsterPrefabFactory : MonoBehaviour
{
	public GameObject[] monsterRef;

	private static BattleMonsterPrefabFactory _instance;
	private BattleMonsterPrefabFactory(){

	}
	public static BattleMonsterPrefabFactory getInstance(){
		if (_instance == null) {
			_instance=GameObject.Find ("BattleMonsterPrefabFactory").gameObject.GetComponent<BattleMonsterPrefabFactory>();
			_instance.Init();
		}
		return _instance;
	}

	void Init(){

	}

	public BaseMonsterShower createMonsterShower(int typeIndex){
		switch (typeIndex) {
		default:
			TestMonsterShower testShower = Instantiate(monsterRef[0]) as TestMonsterShower;
			return testShower;
		}
	}
	
}

