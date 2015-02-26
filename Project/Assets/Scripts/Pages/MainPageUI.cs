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
	}

	public void OnTestFunction2(){
		TimerHelper.getInstance ().DelayFunc (2.0f, delegate() {
			TestJsonLit ();
		});
	}

	void TestJsonLit(){
		string jsonTxt=TextUtils.getInstance().ReadTextFromResources("Texts/GameData/test_json");
		Debug.Log(jsonTxt);
		TestJsonData tjd = JsonMapper.ToObject<TestJsonData>(jsonTxt);
		Debug.Log(tjd.player_name);
		Debug.Log(tjd.player_records[1].id);
		Debug.Log(tjd.player_unlock.stage2);
	}
}

