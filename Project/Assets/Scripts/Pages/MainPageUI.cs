using UnityEngine;
using System.Collections;
using LitJson;

public class MainPageUI : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
			Debug.Log("This is MainPageUI start");
//			string jsonTxt=TextUtils.getInstance().ReadTextFromResources("Texts/GameData/test_json");
//			Debug.Log(jsonTxt);
//			TestJsonData tjd = JsonMapper.ToObject<TestJsonData>(jsonTxt);
//			Debug.Log(tjd.player_name);
//			Debug.Log(tjd.player_records[1].id);
//			Debug.Log(tjd.player_unlock.stage2);
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void GoToNextPage(){
			Debug.Log("click show test page");
			UISystem.getInstance().showPage("Prefabs/TestPageUI");
		}
}

