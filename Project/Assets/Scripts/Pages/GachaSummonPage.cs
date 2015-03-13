using UnityEngine;
using System.Collections;

public class GachaSummonPage : MonoBehaviour
{
	// main objs
	GameObject m_objCenterProcessInfo;
	GameObject m_objLeftSacrificePart;
	GameObject m_objRightNormalPart;

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

	void Awake()
	{
		Debug.Log("This is GachaSummonPage");
	}

	// Use this for initialization
	void Start ()
	{

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
		UISystem.getInstance ().showLastPage ();
	}

	public void OnSacrifice(){
		Debug.Log("Confirm Sacrifice");
	}

	public void OnChooseNormalSummon(){
		Debug.Log ("Show Normal Summon");
	}

	public void OnChooseSacrificeSummon(){
		Debug.Log ("Show Sacrifice Summon");
	}
}

