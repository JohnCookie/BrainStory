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

	Dictionary<int, UserMonster> leftTeam = new Dictionary<int, UserMonster>();
	Dictionary<int, UserMonster> rightTeam = new Dictionary<int, UserMonster>();

	void Awake()
	{
		
	}

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}
}

