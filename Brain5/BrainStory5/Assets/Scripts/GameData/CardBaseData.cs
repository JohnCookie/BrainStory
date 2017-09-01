using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JCFramework;

// 卡牌基础数据
public class CardBaseData {
	public int card_id { get; set; }
	public string card_name { get; set; }
	public string card_desc { get; set; }
	public int card_type { get; set; }
	public List<int> card_skill { get; set; }
}

public class CardBaseDataCollection{
	public List<CardBaseData> data { get; set; }
}

// 卡牌数据管理类
public class CardBaseInfoHelper:JCSingleton<CardBaseInfoHelper>{
	Dictionary<int, CardBaseData> cardBaseDataDict;

	private CardBaseInfoHelper(){
		cardBaseDataDict = new Dictionary<int, CardBaseData> ();
	}

	public CardBaseData getCardBaseInfo(int id){
		if (cardBaseDataDict.ContainsKey (id)) {
			return cardBaseDataDict [id];
		} else {
			return null;
		}
	}
}