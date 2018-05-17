﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JCFramework;
using LitJson;

// 预设角色基础数据
public class CharacterBaseData {
	public int character_id { get; set; }
	public string character_name { get; set; }
	public string character_story { get; set; }
	public int character_job { get; set; }
	public List<int> character_skill { get; set; }
	public int init_sta { get; set; }
	public int init_agi { get; set; }
	public int init_int { get; set; }
	public int init_spr { get; set; }
	public int init_vit { get; set; }
	public int int_luc { get; set; }
}

public class CharacterBaseDataCollection{
	public List<CharacterBaseData> data { get; set; }
}

// 预设角色s数据管理类
public class CharacterBaseInfoHelper:JCSingleton<CharacterBaseInfoHelper>{
	Dictionary<int, CharacterBaseData> characterBaseDataDict;

	private CharacterBaseInfoHelper(){
		characterBaseDataDict = new Dictionary<int, CharacterBaseData> ();
		TextAsset jsonAsset = ResourceManager.getInstance ().getTextAsset ("Data/character_base");
		CharacterBaseDataCollection characterCollection = JsonMapper.ToObject<CharacterBaseDataCollection> ("{\"data\":"+jsonAsset.text+"}");
		foreach (CharacterBaseData data in characterCollection.data) {
			if (!characterBaseDataDict.ContainsKey (data.character_id)) {
				characterBaseDataDict.Add (data.character_id, data);
			} else {
				LogManager.getInstance().Log("Duplicate character_id:" + data.character_id + " in character_base", LogLevel.Error);
			}
		}
	}

	// 根据id获取
	public CharacterBaseData getCharacterBaseInfo(int id){
		if (characterBaseDataDict.ContainsKey (id)) {
			return characterBaseDataDict [id];
		} else {
			LogManager.getInstance().Log("Can not found character_id:" + id + " in character_base", LogLevel.Error);
			return null;
		}
	}

	// 随机获得一个
	public CharacterBaseData getRandomBaseInfo(){
		List<CharacterBaseData> list = new List<CharacterBaseData> (characterBaseDataDict.Values);
		int index = Random.Range (0, list.Count);
		return list [index];
	}
}