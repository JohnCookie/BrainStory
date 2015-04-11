using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GachaSummonPage : MonoBehaviour
{
	// main objs
	GameObject m_objCenterProcessInfo;
	GameObject m_objLeftSacrificePart;
	GameObject m_objRightNormalPart;

	// 2 choice buttn
	UIButton m_btnShowSacrifice;
	UIButton m_btnShowNormal;

	// tweens
	TweenScale m_tweenLeftPart;
	TweenScale m_tweenRightPart;

	// centerProcess
	UILabel m_labelProcessLabel;

	// leftPart
	UIButton m_btnSacrificeSlot_1;
	UIButton m_btnSacrificeSlot_2;
	UIButton m_btnSacrificeSlot_3;
	UIButton m_btnSacrificeSlot_4;
	UIButton m_btnSacrificeSlot_5;
	GameObject m_objSlotShower_1;
	GameObject m_objSlotShower_2;
	GameObject m_objSlotShower_3;
	GameObject m_objSlotShower_4;
	GameObject m_objSlotShower_5;
	UIButton m_btnSacrificeBtn;
	GameObject[] m_objSlotShowerArr = new GameObject[5];

	// rightPart
	UIButton m_btn10Min;
	UIButton m_btn30Min;
	UIButton m_btn2Hour;
	UIButton m_btn6Hour;
	UIButton m_btn12Hour;

	// backBtn
	UIButton m_btnBack;

	enum SummonPageShowingStatus{
		RootChoice,
		NormalShowing,
		SacrificeShowing
	}
	SummonPageShowingStatus currPageStatus = SummonPageShowingStatus.RootChoice;

	List<long> selectedMonsterList=new List<long>();
	Dictionary<int, long> selectedSlotMonsterDict=new Dictionary<int, long>();

	void Awake()
	{
		Debug.Log("This is GachaSummonPage");
		m_objCenterProcessInfo = transform.FindChild("InProcessHint").gameObject;
		m_objLeftSacrificePart = transform.FindChild("SacrificeSummonPart").gameObject;
		m_objRightNormalPart = transform.FindChild("FreeSummonPart").gameObject;

		m_btnShowNormal = transform.FindChild("CenterChoicePart").FindChild("FreeSummonBtn").GetComponent<UIButton>();
		m_btnShowSacrifice = transform.FindChild("CenterChoicePart").FindChild("SacrificeSummonBtn").GetComponent<UIButton>();

		m_tweenLeftPart = m_objLeftSacrificePart.GetComponent<TweenScale>();
		m_tweenRightPart = m_objRightNormalPart.GetComponent<TweenScale>();

		m_labelProcessLabel = m_objCenterProcessInfo.transform.FindChild("InprocessLabel").GetComponent<UILabel>();

		m_btnSacrificeSlot_1 = m_objLeftSacrificePart.transform.FindChild("SacrificePart").FindChild("Addit_1_Btn").GetComponent<UIButton>();
		m_btnSacrificeSlot_2 = m_objLeftSacrificePart.transform.FindChild("SacrificePart").FindChild("Addit_2_Btn").GetComponent<UIButton>();
		m_btnSacrificeSlot_3 = m_objLeftSacrificePart.transform.FindChild("SacrificePart").FindChild("Addit_3_Btn").GetComponent<UIButton>();
		m_btnSacrificeSlot_4 = m_objLeftSacrificePart.transform.FindChild("SacrificePart").FindChild("Addit_4_Btn").GetComponent<UIButton>();
		m_btnSacrificeSlot_5 = m_objLeftSacrificePart.transform.FindChild("SacrificePart").FindChild("Addit_5_Btn").GetComponent<UIButton>();

		m_objSlotShower_1 = m_objLeftSacrificePart.transform.FindChild("SacrificePart").FindChild("Addit1_withMonster").gameObject;
		m_objSlotShowerArr [0] = m_objSlotShower_1;
		m_objSlotShower_2 = m_objLeftSacrificePart.transform.FindChild("SacrificePart").FindChild("Addit2_withMonster").gameObject;
		m_objSlotShowerArr [1] = m_objSlotShower_2;
		m_objSlotShower_3 = m_objLeftSacrificePart.transform.FindChild("SacrificePart").FindChild("Addit3_withMonster").gameObject;
		m_objSlotShowerArr [2] = m_objSlotShower_3;
		m_objSlotShower_4 = m_objLeftSacrificePart.transform.FindChild("SacrificePart").FindChild("Addit4_withMonster").gameObject;
		m_objSlotShowerArr [3] = m_objSlotShower_4;
		m_objSlotShower_5 = m_objLeftSacrificePart.transform.FindChild("SacrificePart").FindChild("Addit5_withMonster").gameObject;
		m_objSlotShowerArr [4] = m_objSlotShower_5;


		m_btnSacrificeBtn = m_objLeftSacrificePart.transform.FindChild("SacrificePart").FindChild("ConfirmBtn").GetComponent<UIButton>();

		m_btn10Min = m_objRightNormalPart.transform.FindChild("FreeSummonBtns").FindChild("10min").GetComponent<UIButton>();
		m_btn30Min = m_objRightNormalPart.transform.FindChild("FreeSummonBtns").FindChild("30min").GetComponent<UIButton>();
		m_btn2Hour = m_objRightNormalPart.transform.FindChild("FreeSummonBtns").FindChild("2hour").GetComponent<UIButton>();
		m_btn6Hour = m_objRightNormalPart.transform.FindChild("FreeSummonBtns").FindChild("6hour").GetComponent<UIButton>();
		m_btn12Hour = m_objRightNormalPart.transform.FindChild("FreeSummonBtns").FindChild("12hour").GetComponent<UIButton>();

		m_btnBack = transform.FindChild("BackBtn").GetComponent<UIButton>();
	}

	// Use this for initialization
	void Start ()
	{
		m_objLeftSacrificePart.SetActive(false);
		m_objRightNormalPart.SetActive(false);
		currPageStatus = SummonPageShowingStatus.RootChoice;
		selectedMonsterList.Clear ();
		selectedSlotMonsterDict.Clear ();
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void On10MinSummon(){
		Debug.Log("Summon 10 Min");
		List<int> m_summonResult = SummonDataUtility.getInstance ().getNormalCard (NormalSummonType.NORMAL_SUMMON_10_MIN);
		string gachas = "";
		for (int i=0; i<m_summonResult.Count; i++) {
			gachas += m_summonResult[i].ToString()+" ";
		}
		Debug.Log ("GachaResult: " + gachas);
		ShowSummonResult(m_summonResult);
	}
	public void On30MinSummon(){
		Debug.Log ("Summon 30 Min");
		List<int> m_summonResult = SummonDataUtility.getInstance ().getNormalCard (NormalSummonType.NORMAL_SUMMON_30_MIN);
		string gachas = "";
		for (int i=0; i<m_summonResult.Count; i++) {
			gachas += m_summonResult[i].ToString()+" ";
		}
		Debug.Log ("GachaResult: " + gachas);
		ShowSummonResult(m_summonResult);
	}
	public void On2HourSummon(){
		Debug.Log ("Summon 2 Hour");
		List<int> m_summonResult = SummonDataUtility.getInstance ().getNormalCard (NormalSummonType.NORMAL_SUMMON_2_HOUR);
		string gachas = "";
		for (int i=0; i<m_summonResult.Count; i++) {
			gachas += m_summonResult[i].ToString()+" ";
		}
		Debug.Log ("GachaResult: " + gachas);
		ShowSummonResult(m_summonResult);
	}
	public void On6HourSummon(){
		Debug.Log ("Summon 6 Hour");
		List<int> m_summonResult = SummonDataUtility.getInstance ().getNormalCard (NormalSummonType.NORMAL_SUMMON_6_HOUR);
		string gachas = "";
		for (int i=0; i<m_summonResult.Count; i++) {
			gachas += m_summonResult[i].ToString()+" ";
		}
		Debug.Log ("GachaResult: " + gachas);
		ShowSummonResult(m_summonResult);
	}
	public void On12HourSummon(){
		Debug.Log ("Summon 12 Hour");
		List<int> m_summonResult = SummonDataUtility.getInstance ().getNormalCard (NormalSummonType.NORMAL_SUMMON_12_HOUR);
		string gachas = "";
		for (int i=0; i<m_summonResult.Count; i++) {
			gachas += m_summonResult[i].ToString()+" ";
		}
		Debug.Log ("GachaResult: " + gachas);
		ShowSummonResult(m_summonResult);
	}

	public void OnSelectSacrificeSlot1(){
		showSelectMonsterPage (1);
	}
	public void OnSelectSacrificeSlot2(){
		showSelectMonsterPage (2);
	}
	public void OnSelectSacrificeSlot3(){
		showSelectMonsterPage (3);
	}
	public void OnSelectSacrificeSlot4(){
		showSelectMonsterPage (4);
	}
	public void OnSelectSacrificeSlot5(){
		showSelectMonsterPage (5);
	}

	public void OnBack(){
		if(currPageStatus != SummonPageShowingStatus.RootChoice){
			if(currPageStatus == SummonPageShowingStatus.NormalShowing){
				ShowNormalSummon(false);
			}
			if(currPageStatus == SummonPageShowingStatus.SacrificeShowing){
				ShowSacrificeSummon(false);
			}
		}else{
			UISystem.getInstance ().showLastPage ();
		}
	}

	public void OnSacrifice(){
		Debug.Log("Confirm Sacrifice");
		List<int> m_summonResult = SummonDataUtility.getInstance().getSacrificeSummonCard(selectedMonsterList);
		string gachas = "";
		for (int i=0; i<m_summonResult.Count; i++) {
			gachas += m_summonResult[i].ToString()+" ";
		}
		Debug.Log ("GachaResult: " + gachas);
		ShowSummonResult(m_summonResult);
		// clear slot (have been deleted in summon process)
		selectedMonsterList.Clear ();
		selectedSlotMonsterDict.Clear ();
		for (int i=1; i<=5; i++) {
			setSacrificeSlot(i,null);
		}
	}

	public void OnChooseNormalSummon(){
		Debug.Log ("Show Normal Summon");
		ShowNormalSummon(true);
	}

	public void OnChooseSacrificeSummon(){
		Debug.Log ("Show Sacrifice Summon");
		ShowSacrificeSummon(true);
	}

	void ShowNormalSummon(bool isShow){
		m_tweenRightPart.enabled=true;
		m_tweenRightPart.ResetToBeginning();
		if(isShow){
			m_objRightNormalPart.SetActive(true);
			m_objCenterProcessInfo.SetActive(false);
			m_tweenRightPart.PlayForward();
			m_btnShowSacrifice.enabled=false;
			currPageStatus = SummonPageShowingStatus.NormalShowing;
		}else{
			m_objRightNormalPart.SetActive(false);
			m_objCenterProcessInfo.SetActive(true);
			m_btnShowSacrifice.enabled=true;
			currPageStatus = SummonPageShowingStatus.RootChoice;
		}
	}

	void ShowSacrificeSummon(bool isShow){
		m_tweenLeftPart.enabled=true;
		m_tweenLeftPart.ResetToBeginning();
		if(isShow){
			m_objLeftSacrificePart.SetActive(true);
			m_objCenterProcessInfo.SetActive(false);
			m_tweenLeftPart.PlayForward();
			m_btnShowNormal.enabled=false;
			currPageStatus = SummonPageShowingStatus.SacrificeShowing;
		}else{
			m_objLeftSacrificePart.SetActive(false);
			m_objCenterProcessInfo.SetActive(true);
			m_btnShowNormal.enabled=true;
			currPageStatus = SummonPageShowingStatus.RootChoice;
		}
	}

	void ShowSummonResult(List<int> summonResult){
		UISystem.getInstance().showPage("Prefabs/SummonResultPage", (page)=>{
			SummonResultPage pageScript = page.GetComponent<SummonResultPage>();
			pageScript.Init(summonResult);
		});
	}

	void showSelectMonsterPage(int slotId){
		Debug.Log ("Select Slot "+slotId);
		UISystem.getInstance ().showPage ("Prefabs/SelectMonsterPageUI", (page) => {
			SelectMonsterPage pageScript = page.GetComponent<SelectMonsterPage>();
			List<UserMonster> initList = new List<UserMonster>();
			for(int i=0;i<UserDataGenerater.GetInstance().UserMonsterDataList.Count;i++){
				if(!selectedMonsterList.Contains(UserDataGenerater.GetInstance().UserMonsterDataList[i].id)){
					initList.Add(UserDataGenerater.GetInstance().UserMonsterDataList[i]);
				}
			}
			pageScript.Init(initList, slotId, OnMonsterSelectedCallback);
		});
	}

	void OnMonsterSelectedCallback(string msg){
		Debug.Log ("monster being selected");
		string[] _info = msg.Split('_');

		if (selectedSlotMonsterDict.ContainsKey (int.Parse (_info [0]))) {
			// has select monster, del first, then add
			selectedMonsterList.Remove (selectedSlotMonsterDict [int.Parse (_info [0])]);
			selectedSlotMonsterDict[int.Parse (_info [0])] = long.Parse (_info [1]);
			selectedMonsterList.Add(long.Parse(_info[1]));
		} else {
			selectedSlotMonsterDict.Add (int.Parse (_info [0]), long.Parse (_info [1]));
			selectedMonsterList.Add(long.Parse(_info[1]));
		}

		setSacrificeSlot(int.Parse(_info[0]), UserDataGenerater.GetInstance().UserMonsterDataDictionary[long.Parse(_info[1])]);
	}

	void setSacrificeSlot(int _slot, UserMonster _monster){
		if (_monster == null) {
			m_objSlotShowerArr [_slot-1].SetActive (false);
		} else {
			MonsterBase _base = MonsterDataUntility.getInstance ().getMonsterBaseInfoById (_monster.monster_id);
			m_objSlotShowerArr [_slot-1].transform.FindChild ("QualityFrame").GetComponent<UISprite> ().spriteName = ResourceNameHelper.getInstance ().getRoundFrameNameByQuality (_base.quality);
			if(m_objSlotShowerArr [_slot-1].transform.FindChild ("Monster").GetComponent<UISprite> ().atlas.GetSprite(_base.icon.ToString())!=null){
				m_objSlotShowerArr [_slot-1].transform.FindChild ("Monster").GetComponent<UISprite> ().spriteName = _base.icon.ToString();
			}else{
				m_objSlotShowerArr [_slot-1].transform.FindChild ("Monster").GetComponent<UISprite> ().spriteName = "0";
			}
			m_objSlotShowerArr [_slot-1].SetActive (true);
		}
	}
}

