using UnityEngine;
using System.Collections;
using LitJson;
using JCFramework;

public class LitJsonTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TextAsset jsonAsset = ResourceManager.getInstance ().getTextAsset ("Data/card_base");
		CardBaseDataCollection cardCollection = JsonMapper.ToObject<CardBaseDataCollection> ("{\"data\":"+jsonAsset.text+"}");

		Debug.Log (cardCollection.data.Count);
	}

}
