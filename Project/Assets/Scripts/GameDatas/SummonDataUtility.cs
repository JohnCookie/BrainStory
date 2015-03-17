using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public enum NormalSummonType
{
	NORMAL_SUMMON_10_MIN=1,
	NORMAL_SUMMON_30_MIN=2,
	NORMAL_SUMMON_2_HOUR=3,
	NORMAL_SUMMON_6_HOUR=4,
	NORMAL_SUMMON_12_HOUR=5
}

public enum SacrificeSummonType
{
	SACRI_SUMMON_LOW_LEVEL=1,
	SACRI_SUMMON_ATTR_ADJUST=2,
	SACRI_SUMMON_HIGH_LEVEL=3
}

public class SummonDataUtility{
	private static SummonDataUtility _instance;
	JsonData m_NormalSummonData;
	JsonData m_SacrificeSummonData;

	private SummonDataUtility(){
		string m_NormalData_Str = TextUtils.getInstance ().ReadTextFromResources ("Texts/GameData/normal_summon_rate_config");
		if (m_NormalData_Str != null) {
			m_NormalData_Str = m_NormalData_Str.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty);
			m_NormalSummonData = JsonMapper.ToObject(m_NormalData_Str);
		} else {
			Debug.Log("Texts/GameData/normal_summon_rate_config read error");
		}
		string m_SacrificeData_Str = TextUtils.getInstance ().ReadTextFromResources ("Texts/GameData/sacrifice_summon_rate_config");
		if (m_SacrificeData_Str != null) {
			m_SacrificeData_Str = m_SacrificeData_Str.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty);
			m_SacrificeSummonData = JsonMapper.ToObject(m_SacrificeData_Str);
		} else {
			Debug.Log("Texts/GameData/sacrifice_summon_rate_config read error");
		}
	}
	public static SummonDataUtility getInstance(){
		if (_instance == null) {
			_instance = new SummonDataUtility();		
		}
		return _instance;
	}

	// Normal Summon
	public List<int> getNormalCard(NormalSummonType type){
		List<int> resultList = new List<int> ();
		JsonData _info = getNormalSummonInfo (type);
		int guaranteeNum = int.Parse (_info ["guarantee"].ToString ());
		for (int i=0; i<guaranteeNum; i++) {
			// Gacha Guarantee Card
			JsonData guaranteeRate = _info ["guarantee_rate"];
			int qualityResult = getQualityFromRate (guaranteeRate);
			MonsterBase oneMonster = getRandomMonsterByQuality (qualityResult);
			resultList.Add (oneMonster.id);
		}
		int leftMonsterNum = int.Parse (_info ["num"].ToString ()) - int.Parse (_info ["guarantee"].ToString ());
		for (int j=0; j<leftMonsterNum; j++) {
			// Gacha Base
			JsonData baseRate = _info["rate"];
			int qualityResult = getQualityFromRate (baseRate);
			MonsterBase oneMonster = getRandomMonsterByQuality (qualityResult);
			resultList.Add (oneMonster.id);
		}
		return resultList;
	}
	

	JsonData getNormalSummonInfo(NormalSummonType type){
		return m_NormalSummonData [((int)type).ToString()];
	}

	int getQualityFromRate(JsonData rateJson){
		int randomNum = Random.Range (1, 101); // [1,101)
		int finalQuality = 0;
		for (int i=0; i<=5; i++) {
			if(randomNum >= int.Parse(rateJson[i.ToString()].ToString())){
				finalQuality = i;
			}else{
				break;
			}
		}
		finalQuality += 1;
		if (finalQuality <= 0 || finalQuality >= 6) {
			Debug.Log("random error, random num="+randomNum+" quality <= 0 or >=6");
			finalQuality = 1;
		}
		return finalQuality;
	}

	MonsterBase getRandomMonsterByQuality(int _quality){
		List<MonsterBase> m_listMonster = MonsterDataUntility.getInstance ().getMonsterInfosByQuality (_quality);
		int randomNum = Random.Range (0, m_listMonster.Count);
		MonsterBase _monster = m_listMonster [randomNum];
		return _monster;
	}
}