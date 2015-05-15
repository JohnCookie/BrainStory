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
			_instance=GameObject.Find ("BattleMonsterFactory").gameObject.GetComponent<BattleMonsterPrefabFactory>();
			_instance.Init();
		}
		return _instance;
	}

	void Init(){

	}

	public BaseMonsterShower createMonsterShower(int typeIndex){
		switch (typeIndex) {
		default:
			GameObject testShower = Instantiate(monsterRef[0]) as GameObject;
			testShower.name = "TestConcreteMonsterShower";
			testShower.transform.localPosition = Vector3.zero;
			testShower.transform.localScale = Vector3.one;
			TestConcreteMonsterShower testConcreteMonsterShower = testShower.GetComponent<TestConcreteMonsterShower>();
			testConcreteMonsterShower.InitMonster(typeIndex);
			return testConcreteMonsterShower;
		}
	}
	
}

