using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System;

public class UserDataGenerater
{
	private static UserDataGenerater _instance;
	private UserDataGenerater(){
		m_userMonsterData = new JsonData();
		parseMonsterListFromJson ();
	}
	public static UserDataGenerater GetInstance(){
		if(_instance==null){
			_instance = new UserDataGenerater();
		}
		return _instance;
	}

	JsonData m_userMonsterData;
	List<UserMonster> m_listUserMonsters = new List<UserMonster>();
	Dictionary<long, UserMonster> m_dictUserMonsters = new Dictionary<long, UserMonster> ();

	JsonData UserMonsterDataJson{
		get{
			return m_userMonsterData;
		}
	}

	public List<UserMonster> UserMonsterDataList{
		get{
			return m_listUserMonsters;
		}
	}

	void parseMonsterListFromJson(){
		// unknown error
		m_userMonsterData.Add (JsonMapper.ToJson(new UserMonster ()));
		m_userMonsterData.Clear ();
		m_listUserMonsters.Clear ();
		for (int i=0; i<m_userMonsterData.Count; i++) {
			UserMonster m = JsonMapper.ToObject<UserMonster>(m_userMonsterData[i].ToJson());
			m_listUserMonsters.Add(m);
			m_dictUserMonsters.Add(m.id, m);
		}
	}

	public void AddNewMonsterById(int id){
		UserMonster newMonster = new UserMonster ();
		newMonster.id = GetNumberOID();
		newMonster.monster_id = id;
		newMonster.exp=0;
		newMonster.skills=new List<int>();
		newMonster.talents=new List<int>();
		m_listUserMonsters.Add (newMonster);
		m_dictUserMonsters.Add (newMonster.id, newMonster);
	}

	public void DelMonsterByUID(long id){
		List<UserMonster> tempMonstersList = new List<UserMonster>();
		for(int i=0;i<m_listUserMonsters.Count;i++){
			UserMonster m_monster = m_listUserMonsters[i];
			if(m_monster.id != id){
				// find the monster
				tempMonstersList.Add(m_monster);
			}
		}
		m_listUserMonsters.Clear();
		m_dictUserMonsters.Clear ();
		foreach(UserMonster m in tempMonstersList){
			m_listUserMonsters.Add(m);
			m_dictUserMonsters.Add(m.id, m);
		}
	}

	private long GetNumberOID()
	{
		byte[] buffer = Guid.NewGuid().ToByteArray();
		return BitConverter.ToInt64(buffer, 0);
	}

	public void synMonsterJson(){
		m_userMonsterData = JsonMapper.ToJson(m_listUserMonsters);
	}

	public void printCurrMonsters(){
		synMonsterJson ();
		Debug.Log (m_userMonsterData.ToJson ());
	}
}

