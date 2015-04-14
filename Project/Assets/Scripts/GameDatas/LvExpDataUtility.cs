using System;
using LitJson;
using UnityEngine;

public class LvExpDataUtility
{
	private static LvExpDataUtility _instance;
	JsonData m_jsonData;
	public static string LvExpKey = "lv_exp";
	public static string MonsterExpKey = "monster_exp";
	public static string LvReduceKey = "lv_reduce";

	private LvExpDataUtility(){
		string m_str = TextUtils.getInstance ().ReadTextFromResources ("Texts/GameData/exp_lv_config");
		if (m_str != null) {
			m_str = m_str.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty);
			m_jsonData = JsonMapper.ToObject(m_str);
		} else {
			Debug.Log("Texts/GameData/exp_lv_config read error");
		}
	}
	public static LvExpDataUtility getInstance(){
		if (_instance == null) {
			_instance = new LvExpDataUtility();		
		}
		return _instance;
	}

	public int getMonsterLv(UserMonster _monster){
		return getLvByExp(_monster.exp);
	}

	public int getLvByExp(int _exp){
		JsonData expRequireJson = m_jsonData[LvExpKey];
		for (int i=10; i>0; i--) {
			if(_exp >= int.Parse(expRequireJson[i.ToString()].ToString())){
				return i;
			}
		}
		return 0;
	}

	public int getMonsterExp(UserMonster _monster){
		return getExpByQuality (MonsterDataUntility.getInstance ().getMonsterBaseInfoById (_monster.monster_id).quality);
	}

	public int getExpByQuality(int quality){
		JsonData monsterExp = m_jsonData [MonsterExpKey];
		return int.Parse (monsterExp [quality.ToString ()].ToString ());
	}

	public double getReduceByLv(int lv_diff){
		JsonData reduce_json = m_jsonData [LvReduceKey];
		if (lv_diff < 4) {
			return double.Parse(reduce_json[lv_diff.ToString()].ToString());		
		} else {
			return double.Parse(reduce_json["4"].ToString());
		}
	}
}

