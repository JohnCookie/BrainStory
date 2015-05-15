using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimulatorBattleArrangePage : MonoBehaviour
{
	UIButton m_btnBack;
	UIButton m_btnStart;

	UIButton m_leftPos0;
	UIButton m_leftPos1;
	UIButton m_leftPos2;
	UIButton m_leftPos3;
	UIButton m_leftPos4;
	UIButton m_leftPos5;
	UIButton m_leftPos6;
	UIButton m_leftPos7;
	UIButton m_leftPos8;

	UIButton m_rightPos0;
	UIButton m_rightPos1;
	UIButton m_rightPos2;
	UIButton m_rightPos3;
	UIButton m_rightPos4;
	UIButton m_rightPos5;
	UIButton m_rightPos6;
	UIButton m_rightPos7;
	UIButton m_rightPos8;

	List<long> selectedMonsterList = new List<long> ();
	Dictionary<int, long> leftTeam = new Dictionary<int, long>();
	Dictionary<int, long> rightTeam = new Dictionary<int, long>();
	UIButton[] m_leftTeamBtns = new UIButton[9];
	UIButton[] m_rightTeamBtns = new UIButton[9];

	void Awake()
	{
		Transform leftTeamNode = transform.FindChild ("LeftTeam").FindChild ("Monsters");
		Transform rightTeamNode = transform.FindChild ("RightTeam").FindChild ("Monsters");
		m_leftPos0 = leftTeamNode.FindChild ("Monster_0").GetComponent<UIButton> ();
		m_leftPos1 = leftTeamNode.FindChild ("Monster_1").GetComponent<UIButton> ();
		m_leftPos2 = leftTeamNode.FindChild ("Monster_2").GetComponent<UIButton> ();
		m_leftPos3 = leftTeamNode.FindChild ("Monster_3").GetComponent<UIButton> ();
		m_leftPos4 = leftTeamNode.FindChild ("Monster_4").GetComponent<UIButton> ();
		m_leftPos5 = leftTeamNode.FindChild ("Monster_5").GetComponent<UIButton> ();
		m_leftPos6 = leftTeamNode.FindChild ("Monster_6").GetComponent<UIButton> ();
		m_leftPos7 = leftTeamNode.FindChild ("Monster_7").GetComponent<UIButton> ();
		m_leftPos8 = leftTeamNode.FindChild ("Monster_8").GetComponent<UIButton> ();
		m_rightPos0 = rightTeamNode.FindChild ("Monster_0").GetComponent<UIButton> ();
		m_rightPos1 = rightTeamNode.FindChild ("Monster_1").GetComponent<UIButton> ();
		m_rightPos2 = rightTeamNode.FindChild ("Monster_2").GetComponent<UIButton> ();
		m_rightPos3 = rightTeamNode.FindChild ("Monster_3").GetComponent<UIButton> ();
		m_rightPos4 = rightTeamNode.FindChild ("Monster_4").GetComponent<UIButton> ();
		m_rightPos5 = rightTeamNode.FindChild ("Monster_5").GetComponent<UIButton> ();
		m_rightPos6 = rightTeamNode.FindChild ("Monster_6").GetComponent<UIButton> ();
		m_rightPos7 = rightTeamNode.FindChild ("Monster_7").GetComponent<UIButton> ();
		m_rightPos8 = rightTeamNode.FindChild ("Monster_8").GetComponent<UIButton> ();
		m_leftTeamBtns [0] = m_leftPos0;
		m_leftTeamBtns [1] = m_leftPos1;
		m_leftTeamBtns [2] = m_leftPos2;
		m_leftTeamBtns [3] = m_leftPos3;
		m_leftTeamBtns [4] = m_leftPos4;
		m_leftTeamBtns [5] = m_leftPos5;
		m_leftTeamBtns [6] = m_leftPos6;
		m_leftTeamBtns [7] = m_leftPos7;
		m_leftTeamBtns [8] = m_leftPos8;
		m_rightTeamBtns [0] = m_rightPos0;
		m_rightTeamBtns [1] = m_rightPos1;
		m_rightTeamBtns [2] = m_rightPos2;
		m_rightTeamBtns [3] = m_rightPos3;
		m_rightTeamBtns [4] = m_rightPos4;
		m_rightTeamBtns [5] = m_rightPos5;
		m_rightTeamBtns [6] = m_rightPos6;
		m_rightTeamBtns [7] = m_rightPos7;
		m_rightTeamBtns [8] = m_rightPos8;

		m_btnBack = transform.FindChild ("BackBtn").GetComponent<UIButton> ();
		m_btnStart = transform.FindChild ("StartBtn").GetComponent<UIButton> ();
	}

	// Use this for initialization
	void Start ()
	{
		selectedMonsterList.Clear ();
		leftTeam.Clear ();
		rightTeam.Clear ();
	}
		
	public void OnSelectLeft1(){
		SelectTeamMonster (0);
	}
	public void OnSelectLeft2(){
		SelectTeamMonster (1);
	}
	public void OnSelectLeft3(){
		SelectTeamMonster (2);
	}
	public void OnSelectLeft4(){
		SelectTeamMonster (3);
	}
	public void OnSelectLeft5(){
		SelectTeamMonster (4);
	}
	public void OnSelectLeft6(){
		SelectTeamMonster (5);
	}
	public void OnSelectLeft7(){
		SelectTeamMonster (6);
	}
	public void OnSelectLeft8(){
		SelectTeamMonster (7);
	}
	public void OnSelectLeft9(){
		SelectTeamMonster (8);
	}
	public void OnSelectRight1(){
		SelectTeamMonster (9);
	}
	public void OnSelectRight2(){
		SelectTeamMonster (10);	
	}
	public void OnSelectRight3(){
		SelectTeamMonster (11);
	}
	public void OnSelectRight4(){
		SelectTeamMonster (12);
	}
	public void OnSelectRight5(){
		SelectTeamMonster (13);
	}
	public void OnSelectRight6(){
		SelectTeamMonster (14);
	}
	public void OnSelectRight7(){
		SelectTeamMonster (15);
	}
	public void OnSelectRight8(){
		SelectTeamMonster (16);
	}
	public void OnSelectRight9(){
		SelectTeamMonster (17);
	}
	void SelectTeamMonster(int index){
		UISystem.getInstance ().showPage ("Prefabs/SelectMonsterPageUI", (page) => {
			SelectMonsterPage pageScript = page.GetComponent<SelectMonsterPage>();
			List<UserMonster> initList = new List<UserMonster>();
			for(int i=0;i<UserDataGenerater.GetInstance().UserMonsterDataList.Count;i++){
				if(!selectedMonsterList.Contains(UserDataGenerater.GetInstance().UserMonsterDataList[i].id)){
					initList.Add(UserDataGenerater.GetInstance().UserMonsterDataList[i]);
				}
			}
			pageScript.Init(initList, index, OnMonsterSelectedCallback);
		});
	}

	void OnMonsterSelectedCallback(string msg){
		string[] _info = msg.Split('_');
		int pos_index = int.Parse(_info [0]);
		long umid = long.Parse (_info [1]);
		if (pos_index < 9) {
			//left
			if(leftTeam.ContainsKey(pos_index)){
				selectedMonsterList.Remove(leftTeam[pos_index]);
				leftTeam[pos_index]=umid;
				selectedMonsterList.Add(umid);
			}else{
				leftTeam.Add(pos_index,umid);
				selectedMonsterList.Add(umid);
			}
		} else {
			//right
			if(rightTeam.ContainsKey(pos_index-9)){
				selectedMonsterList.Remove(rightTeam[pos_index-9]);
				rightTeam[pos_index-9]=umid;
				selectedMonsterList.Add(umid);
			}else{
				rightTeam.Add(pos_index-9,umid);
				selectedMonsterList.Add(umid);
			}
		}
		
		setTeamMonster(pos_index, UserDataGenerater.GetInstance().UserMonsterDataDictionary[umid]);
	}

	void setTeamMonster(int index, UserMonster monster){
		if (monster == null) {
			if(index<9){
				//left
				m_leftTeamBtns[index].transform.FindChild("add").gameObject.SetActive(true);
				m_leftTeamBtns[index].transform.FindChild("monster").gameObject.SetActive(false);
			}else{
				//right
				m_rightTeamBtns[index-9].transform.FindChild("add").gameObject.SetActive(true);
				m_rightTeamBtns[index-9].transform.FindChild("monster").gameObject.SetActive(false);
			}
		} else {
			MonsterBase _base = MonsterDataUntility.getInstance ().getMonsterBaseInfoById (monster.monster_id);
			if(index<9){
				//left
				m_leftTeamBtns[index].transform.FindChild("add").gameObject.SetActive(false);
				if(m_leftTeamBtns[index].transform.FindChild("monster").GetComponent<UISprite>().atlas.GetSprite(_base.icon.ToString())!=null){
					m_leftTeamBtns[index].transform.FindChild("monster").GetComponent<UISprite>().spriteName = _base.icon.ToString();
				}else{
					m_leftTeamBtns[index].transform.FindChild("monster").GetComponent<UISprite>().spriteName = "0";
				}
				m_leftTeamBtns[index].transform.FindChild("monster").gameObject.SetActive(true);
			}else{
				//right
				m_rightTeamBtns[index-9].transform.FindChild("add").gameObject.SetActive(false);
				if(m_rightTeamBtns[index-9].transform.FindChild("monster").GetComponent<UISprite>().atlas.GetSprite(_base.icon.ToString())!=null){
					m_rightTeamBtns[index-9].transform.FindChild("monster").GetComponent<UISprite>().spriteName = _base.icon.ToString();
				}else{
					m_rightTeamBtns[index-9].transform.FindChild("monster").GetComponent<UISprite>().spriteName = "0";
				}
				m_rightTeamBtns[index-9].transform.FindChild("monster").gameObject.SetActive(true);
			}
		}
	}

	public void OnBack(){
		UISystem.getInstance().showLastPage();
	}

	public void OnStartBattle(){
		Dictionary<int, UserMonster> leftTeamInfo = new Dictionary<int, UserMonster> ();
		Dictionary<int, UserMonster> rightTeamInfo = new Dictionary<int, UserMonster> ();
		foreach (int index in leftTeam.Keys) {
			leftTeamInfo.Add(index, UserDataGenerater.GetInstance().getUserMonsterByUniqueId(leftTeam[index]));
		}
		foreach (int index in rightTeam.Keys) {
			rightTeamInfo.Add(index, UserDataGenerater.GetInstance().getUserMonsterByUniqueId(rightTeam[index]));	
		}
		BattleSimulateTest battleSimulater = new BattleSimulateTest ();
		battleSimulater.simulateBattle (leftTeamInfo, rightTeamInfo);

		string battleReport = BattleReportGenerater.getInstance ().getWholeJsonStr ();
		Debug.Log (battleReport);

		PlayerPrefs.SetString ("battle_report", battleReport);
		UISystem.getInstance ().showPage ("Prefabs/BattleReplayPage");
	}
}

