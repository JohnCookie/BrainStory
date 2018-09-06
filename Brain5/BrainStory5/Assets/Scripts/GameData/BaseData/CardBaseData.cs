using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JCFramework;
using LitJson;

// 卡牌基础数据
public class CardBaseData {
	public int card_id { get; set; }
	public int card_cost { get; set; }
	public string card_name { get; set; }
	public string card_desc { get; set; }
	public int card_job { get; set; }
	public List<int> card_skill { get; set; }
	public float cast_time { get; set; }
	public string card_story { get; set; }
	public string card_img { get; set; }
}

public class CardBaseDataCollection{
	public List<CardBaseData> data { get; set; }
}

// 卡牌数据管理类
public class CardBaseInfoHelper:JCSingleton<CardBaseInfoHelper>{
	Dictionary<int, CardBaseData> cardBaseDataDict;

	private CardBaseInfoHelper(){
		cardBaseDataDict = new Dictionary<int, CardBaseData> ();
		TextAsset jsonAsset = ResourceManager.getInstance ().getTextAsset ("Data/card_base");
		CardBaseDataCollection cardCollection = JsonMapper.ToObject<CardBaseDataCollection> ("{\"data\":"+jsonAsset.text+"}");
		foreach (CardBaseData data in cardCollection.data) {
			if (!cardBaseDataDict.ContainsKey (data.card_id)) {
				cardBaseDataDict.Add (data.card_id, data);
			} else {
				LogManager.getInstance().Log("Duplicate card_id:" + data.card_id + " in card_base", LogLevel.Error);
			}
		}
	}

	public CardBaseData getCardBaseInfo(int id){
		if (cardBaseDataDict.ContainsKey (id)) {
			return cardBaseDataDict [id];
		} else {
			LogManager.getInstance().Log("Can not found card_id:" + id + " in card_base", LogLevel.Error);
			return null;
		}
	}
}