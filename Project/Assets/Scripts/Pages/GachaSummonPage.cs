using UnityEngine;
using System.Collections;

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
		m_objSlotShower_2 = m_objLeftSacrificePart.transform.FindChild("SacrificePart").FindChild("Addit2_withMonster").gameObject;
		m_objSlotShower_3 = m_objLeftSacrificePart.transform.FindChild("SacrificePart").FindChild("Addit3_withMonster").gameObject;
		m_objSlotShower_4 = m_objLeftSacrificePart.transform.FindChild("SacrificePart").FindChild("Addit4_withMonster").gameObject;
		m_objSlotShower_5 = m_objLeftSacrificePart.transform.FindChild("SacrificePart").FindChild("Addit5_withMonster").gameObject;

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
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void On10MinSummon(){
		Debug.Log("Summon 10 Min");
	}
	public void On30MinSummon(){
		Debug.Log ("Summon 30 Min");
	}
	public void On2HourSummon(){
		Debug.Log ("Summon 2 Hour");
	}
	public void On6HourSummon(){
		Debug.Log ("Summon 6 Hour");
	}
	public void On12HourSummon(){
		Debug.Log ("Summon 12 Hour");
	}

	public void OnSelectSacrificeSlot1(){
		Debug.Log ("Select Slot 1");
	}
	public void OnSelectSacrificeSlot2(){
		Debug.Log ("Select Slot 2");
	}
	public void OnSelectSacrificeSlot3(){
		Debug.Log ("Select Slot 3");
	}
	public void OnSelectSacrificeSlot4(){
		Debug.Log ("Select Slot 4");
	}
	public void OnSelectSacrificeSlot5(){
		Debug.Log ("Select Slot 5");
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
}

