using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

// Base attack type
public enum MonsterAtkType{
	Sharp=1,
	Blunt=2,
	Arrow=3,
	Magic=4
}

// Base enchant type
public enum MonsterAtkEnchant{
	NoEnchant,
	Fire,
	Ice,
	Thunder,
	Natural,
	Holy,
	Curse,
	Poison
}

// Base defence type
public enum MonsterDefType{
	NoArmor=1,
	LightArmor=2,
	HeavyArmor=3,
	EnhancedArmor=4
}


public class MonsterDataUntility{
	private static MonsterDataUntility _instance = null;

	MonsterBaseList m_monsterBaseInfoList = null;
	Dictionary<int, MonsterBase> m_dictIdToMonster = new Dictionary<int, MonsterBase>();
	List<MonsterBase> m_listOneStarMonsters = new List<MonsterBase>();
	List<MonsterBase> m_listTwoStarMonsters = new List<MonsterBase>();
	List<MonsterBase> m_listThreeStarMonsters = new List<MonsterBase>();
	List<MonsterBase> m_listFourStarMonsters = new List<MonsterBase>();
	List<MonsterBase> m_listFiveStarMonsters = new List<MonsterBase>();
	Dictionary<int, List<MonsterBase>> m_dictStarToMonsters = new Dictionary<int, List<MonsterBase>>();


	private MonsterDataUntility(){
		m_monsterBaseInfoList = JsonMapper.ToObject<MonsterBaseList>(TextUtils.getInstance ().ReadTextFromResources ("Texts/GameData/monster_base"));
		if (m_monsterBaseInfoList != null) {
			InitOtherDataStructs ();
		} else {
			Debug.Log("Texts/GameData/monster_base read error");
		}
	}
	public static MonsterDataUntility getInstance(){
		if (_instance == null) {
			_instance = new MonsterDataUntility();
		}
		return _instance;
	}

	void InitOtherDataStructs(){
		m_dictIdToMonster.Clear ();
		m_dictStarToMonsters.Clear ();
		for (int i=0; i<m_monsterBaseInfoList.data.Count; i++) {
			m_dictIdToMonster.Add(m_monsterBaseInfoList.data[i].id, m_monsterBaseInfoList.data[i]);
			switch(m_monsterBaseInfoList.data[i].quality){
			case 1:
				m_listOneStarMonsters.Add(m_monsterBaseInfoList.data[i]);
				break;
			case 2:
				m_listTwoStarMonsters.Add(m_monsterBaseInfoList.data[i]);
				break;
			case 3:
				m_listThreeStarMonsters.Add(m_monsterBaseInfoList.data[i]);
				break;
			case 4:
				m_listFourStarMonsters.Add(m_monsterBaseInfoList.data[i]);
				break;
			case 5:
				m_listFiveStarMonsters.Add(m_monsterBaseInfoList.data[i]);
				break;
			default:
				break;
			}
		}
		m_dictStarToMonsters.Add (1, m_listOneStarMonsters);
		m_dictStarToMonsters.Add (2, m_listTwoStarMonsters);
		m_dictStarToMonsters.Add (3, m_listThreeStarMonsters);
		m_dictStarToMonsters.Add (4, m_listFourStarMonsters);
		m_dictStarToMonsters.Add (5, m_listFiveStarMonsters);
	}

	public MonsterBase getMonsterBaseInfoById(int id){
		return m_dictIdToMonster[id];
	}

	public List<MonsterBase> getMonsterInfosByQuality(int quality){
		return m_dictStarToMonsters[quality];
	}

	public List<MonsterBase> getAllMonster(){
		return m_monsterBaseInfoList.data;
	}
}