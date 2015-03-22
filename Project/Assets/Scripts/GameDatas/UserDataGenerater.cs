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
	}
	public static UserDataGenerater GetInstance(){
		if(_instance==null){
			_instance = new UserDataGenerater();
		}
		return _instance;
	}

	JsonData m_userMonsterData; 

	public JsonData UserMonsterData{
		get{
			return m_userMonsterData;
		}
	}

	public void AddNewMonsterById(int id){
		UserMonster newMonster = new UserMonster();
		newMonster.id = GetNumberOID();
		newMonster.monster_id = id;
		newMonster.exp=0;
		newMonster.skills=new List<int>();
		newMonster.talents=new List<int>();
		JsonData newMonsterJson = JsonMapper.ToJson(newMonster);
		Debug.Log(newMonsterJson.ToJson());
		m_userMonsterData.Add(newMonsterJson);
		Debug.Log(m_userMonsterData.ToJson());
	}

	public void DelMonsterByUID(long id){
		List<UserMonster> tempMonstersList = new List<UserMonster>();
		for(int i=0;i<m_userMonsterData.Count;i++){
			UserMonster m_monster = JsonMapper.ToObject<UserMonster>(m_userMonsterData[i].ToString());
			if(m_monster.id != id){
				// find the monster
				tempMonstersList.Add(m_monster);
			}
		}
		m_userMonsterData.Clear();
		foreach(UserMonster m in tempMonstersList){
			m_userMonsterData.Add(JsonMapper.ToJson(m));
		}
		Debug.Log(m_userMonsterData.ToJson());
	}

	private long GetNumberOID()
	{
		byte[] buffer = Guid.NewGuid().ToByteArray();
		return BitConverter.ToInt64(buffer, 0);
	} 
}

