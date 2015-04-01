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

			UserDataGenerater.GetInstance().AddNewMonsterById(oneMonster.id);
		}
		int leftMonsterNum = int.Parse (_info ["num"].ToString ()) - int.Parse (_info ["guarantee"].ToString ());
		for (int j=0; j<leftMonsterNum; j++) {
			// Gacha Base
			JsonData baseRate = _info["rate"];
			int qualityResult = getQualityFromRate (baseRate);
			MonsterBase oneMonster = getRandomMonsterByQuality (qualityResult);
			resultList.Add (oneMonster.id);

			UserDataGenerater.GetInstance().AddNewMonsterById(oneMonster.id);
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
		Debug.Log("roll: "+randomNum);
		return finalQuality;
	}

	MonsterBase getRandomMonsterByQuality(int _quality){
		List<MonsterBase> m_listMonster = MonsterDataUntility.getInstance ().getMonsterInfosByQuality (_quality);
		int randomNum = Random.Range (0, m_listMonster.Count);
		MonsterBase _monster = m_listMonster [randomNum];
		return _monster;
	}

	// Sacrifice Summon
	public List<int> getSacrificeSummonCard(List<long> sacrificeMonsterList){
		List<int> resultList;
		int star4MonsterNum=0;
		int star5MonsterNum=0;
		int totalStarNum=0;

		for(int i=0;i<sacrificeMonsterList.Count;i++){
			UserMonster _m = UserDataGenerater.GetInstance().UserMonsterDataDictionary[sacrificeMonsterList[i]];
			MonsterBase _mb = MonsterDataUntility.getInstance().getMonsterBaseInfoById(_m.monster_id);
			if(_mb.quality==4){
				star4MonsterNum++;
			}
			if(_mb.quality==5){
				star5MonsterNum++;
			}
			totalStarNum+=_mb.quality;
		}

		if(star4MonsterNum+star5MonsterNum==0){
			// low
			resultList = getSacrificedCardByType(SacrificeSummonType.SACRI_SUMMON_LOW_LEVEL, totalStarNum, star4MonsterNum, star5MonsterNum);
		}else if (star4MonsterNum+star5MonsterNum>3){
			// high
			resultList = getSacrificedCardByType(SacrificeSummonType.SACRI_SUMMON_HIGH_LEVEL, totalStarNum, star4MonsterNum, star5MonsterNum);
		}else{
			// normal
			resultList = getSacrificedCardByType(SacrificeSummonType.SACRI_SUMMON_ATTR_ADJUST, totalStarNum, star4MonsterNum, star5MonsterNum);
		}

		deleteUserMonster (sacrificeMonsterList);
		return resultList;
	}

	List<int> getSacrificedCardByType(SacrificeSummonType _type, int _starNum, int _star4Num, int _star5Num){
		List<int> resultCards=null;
		switch(_type){
		case SacrificeSummonType.SACRI_SUMMON_LOW_LEVEL:
			resultCards = SacrificeLowLevelMonster(_starNum);
			break;
		case SacrificeSummonType.SACRI_SUMMON_ATTR_ADJUST:
			resultCards = SacrificeAttrAdjustMonster(_star4Num, _star5Num);
			break;
		case SacrificeSummonType.SACRI_SUMMON_HIGH_LEVEL:
			resultCards = SacrificeHighLevelMonster();
			break;
		}
		return resultCards;
	}

	List<int> SacrificeLowLevelMonster(int totalNum){
		List<int> resultList = new List<int> ();
		JsonData _info = getSacrificeSummonInfo (SacrificeSummonType.SACRI_SUMMON_LOW_LEVEL);
		int _infoIndex = 0;
		for(int i=0;i<_info.Count;i++){
			if(totalNum>=int.Parse(_info[i]["star_min"].ToString()) && totalNum<=int.Parse(_info[i]["star_max"].ToString())){
				_infoIndex=i;
			}
		}
		JsonData usedSummonInfo = _info[_infoIndex];
		JsonData baseRate = usedSummonInfo["rate"];
		int qualityResult = getQualityFromRate (baseRate);
		MonsterBase oneMonster = getRandomMonsterByQuality (qualityResult);
		resultList.Add (oneMonster.id);
		
		UserDataGenerater.GetInstance().AddNewMonsterById(oneMonster.id);
		return resultList;
	}

	List<int> SacrificeAttrAdjustMonster(int _star4Num, int _star5Num){
		List<int> resultList = new List<int> ();
		JsonData _info = getSacrificeSummonInfo (SacrificeSummonType.SACRI_SUMMON_ATTR_ADJUST);
		int _infoIndex = 0;
		for(int i=0;i<_info.Count;i++){
			if(_star4Num>=int.Parse(_info[i]["star_4_num"].ToString()) && _star5Num>=int.Parse(_info[i]["star_5_num"].ToString())){
				_infoIndex=i;
			}
		}
		JsonData usedSummonInfo = _info[_infoIndex];
		JsonData baseRate = usedSummonInfo["rate"];
		int qualityResult = getQualityFromRate (baseRate);
		MonsterBase oneMonster = getRandomMonsterByQuality (qualityResult);
		resultList.Add (oneMonster.id);
		
		UserDataGenerater.GetInstance().AddNewMonsterById(oneMonster.id);
		return resultList;
	}

	List<int> SacrificeHighLevelMonster(){
		List<int> resultList = new List<int> ();
		JsonData _info = getSacrificeSummonInfo (SacrificeSummonType.SACRI_SUMMON_HIGH_LEVEL);
		JsonData usedSummonInfo = _info[0];
		JsonData baseRate = usedSummonInfo["rate"];
		int qualityResult = getQualityFromRate (baseRate);
		MonsterBase oneMonster = getRandomMonsterByQuality (qualityResult);
		resultList.Add (oneMonster.id);
		
		UserDataGenerater.GetInstance().AddNewMonsterById(oneMonster.id);
		return resultList;
	}

	void deleteUserMonster(List<long> _userMonsterIds){
		for (int i=0; i<_userMonsterIds.Count; i++) {
			UserDataGenerater.GetInstance().DelMonsterByUID(_userMonsterIds[i]);	
		}
	}

	JsonData getSacrificeSummonInfo(SacrificeSummonType type){
		return m_SacrificeSummonData [((int)type).ToString()];
	}
}