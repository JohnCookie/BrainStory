using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using JCFramework;

public class CharacterStatsUI : BaseView
{
	public Text m_textPlayerName;
	public Text m_textPlayerSta;
	public Text m_textPlayerAgi;
	public Text m_textPlayerInt;
	public Text m_textPlayerSpr;
	public Text m_textPlayerVit;
	public Text m_textPlayerLuc;
	public Image m_imgCharacter;
	public Image m_imgJob;

	CharacterPO currCharacterData;

	public override void InitOnAwake ()
	{
		base.InitOnAwake ();
		// 读取玩家数据
	}

	public override void RefreshView ()
	{
		base.RefreshView ();
		// 刷新玩家列表
	}

	public void Init(CharacterPO data){
		this.currCharacterData = data;

		m_textPlayerName.text = this.currCharacterData.name;
		m_textPlayerSta.text = this.currCharacterData.attrSTA.ToString();
		m_textPlayerAgi.text = this.currCharacterData.attrAGI.ToString();
		m_textPlayerInt.text = this.currCharacterData.attrINT.ToString();
		m_textPlayerSpr.text = this.currCharacterData.attrSPR.ToString();
		m_textPlayerVit.text = this.currCharacterData.attrVIT.ToString();
		m_textPlayerLuc.text = this.currCharacterData.attrLUC.ToString();
		m_imgCharacter.sprite = ResourceManager.getInstance ().getSprite ("Sprite/Character/"+this.currCharacterData.img);
		m_imgJob.sprite = ResourceManager.getInstance ().getSprite ("Sprite/Job/"+this.currCharacterData.job);
	}
}

