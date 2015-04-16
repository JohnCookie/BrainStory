using UnityEngine;
using System.Collections;
using LitJson;

public class MainPageUI : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Debug.Log("This is MainPageUI start");

	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void OnEnterSummonPage(){
		UISystem.getInstance ().showPage ("Prefabs/GachaPageUI");
	}

	public void OnEnterMonsterPage(){
		UISystem.getInstance ().showPage ("Prefabs/UserMonsterBoxPageUI");
	}

	public void OnEnterTaskPage(){
		UISystem.getInstance ().showCommonDialog (CommonDialogStyle.OnlyConfirmStyle, "", "This function will be open later.", null, null, null);
	}

	public void OnEnterAchievementPage(){
		UISystem.getInstance ().showCommonDialog (CommonDialogStyle.OnlyConfirmStyle, "", "This function will be open later.", null, null, null);
	}

	#region for test
	public void OnTestFunction1(){
		Debug.Log("click show test page");
		UISystem.getInstance ().showCommonDialog (CommonDialogStyle.ConfirmCancleStyle, "TITLE", "AAA\n   BBB\n      CCC",
		                                          (string msgLeft) => {
			Debug.Log ("Left Dialog Btn");
		},
		delegate(string msgRight) {
			Debug.Log ("Right Dialog Btn");
		},
		null);
		
		UserDataGenerater.GetInstance().printCurrMonsters();
	}
	
	public void OnTestFunction2(){
		// Test Delay Function
		/* 
		TimerHelper.getInstance ().DelayFunc (2.0f, delegate() {
			TestJsonLit ();
		});
		*/
		// Test Monster Create
		/*
		UserMonster monster_1 = UserDataGenerater.GetInstance().UserMonsterDataList[0];
		long id = monster_1.id;
		Debug.Log(id);
		UserDataGenerater.GetInstance().DelMonsterByUID(id);
		*/
		// Simulate Battle
		BattleSimulateTest battleSimulater = new BattleSimulateTest ();
		battleSimulater.simulate1v1Battle ();
	}

	void TestJsonLit(){
		string jsonTxt=TextUtils.getInstance().ReadTextFromResources("Texts/GameData/test_json");
		Debug.Log(jsonTxt);
		TestJsonData tjd = JsonMapper.ToObject<TestJsonData>(jsonTxt);
		Debug.Log(tjd.player_name);
		Debug.Log(tjd.player_records[1].id);
		Debug.Log(tjd.player_unlock.stage2);
	}
	#endregion
}

