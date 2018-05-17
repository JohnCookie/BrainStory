using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JCFramework;
using LitJson;

// 随机角色基础数据
public class RandomCharacterBaseData {
	public int tplt_id { get; set; }
	public int job { get; set; }
	public int min_sta { get; set; }
	public int max_sta { get; set; }
	public int min_agi { get; set; }
	public int max_agi { get; set; }
	public int min_int { get; set; }
	public int max_int { get; set; }
	public int min_spr { get; set; }
	public int max_spr { get; set; }
	public int min_vit { get; set; }
	public int max_vit { get; set; }
	public int min_luc { get; set; }
	public int max_luc { get; set; }
}

public class RandomCharacterBaseDataCollection{
	public List<RandomCharacterBaseData> data { get; set; }
}

// 随机角色数据管理类
public class RandomCharacterBaseInfoHelper:JCSingleton<RandomCharacterBaseInfoHelper>{
	Dictionary<int, RandomCharacterBaseData> randomCharacterBaseDataDict;

	private RandomCharacterBaseInfoHelper(){
		randomCharacterBaseDataDict = new Dictionary<int, RandomCharacterBaseData> ();
		TextAsset jsonAsset = ResourceManager.getInstance ().getTextAsset ("Data/random_character_base");
		RandomCharacterBaseDataCollection randomCharacterCollection = JsonMapper.ToObject<RandomCharacterBaseDataCollection> ("{\"data\":"+jsonAsset.text+"}");
		foreach (RandomCharacterBaseData data in randomCharacterCollection.data) {
			if (!randomCharacterBaseDataDict.ContainsKey (data.tplt_id)) {
				randomCharacterBaseDataDict.Add (data.tplt_id, data);
			} else {
				LogManager.getInstance().Log("Duplicate random_tplt_id:" + data.tplt_id + " in random_character_base", LogLevel.Error);
			}
		}
	}

	// 通过模板id获取
	public RandomCharacterBaseData getRandomCharacterBaseInfo(int tpltId){
		if (randomCharacterBaseDataDict.ContainsKey (tpltId)) {
			return randomCharacterBaseDataDict [tpltId];
		} else {
			LogManager.getInstance().Log("Can not found tpltId:" + tpltId + " in random_character_base", LogLevel.Error);
			return null;
		}
	}

	// 随机获得一个
	public RandomCharacterBaseData getRandomBaseInfo(){
		List<RandomCharacterBaseData> list = new List<RandomCharacterBaseData> (randomCharacterBaseDataDict.Values);
		int index = Random.Range (0, list.Count);
		return list [index];
	}
}