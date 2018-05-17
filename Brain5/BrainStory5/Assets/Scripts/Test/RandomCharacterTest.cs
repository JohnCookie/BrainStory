using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using JCGame;
using JCFramework;

public class RandomCharacterTest : MonoBehaviour
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

	public void generateOneCharacter(){
		int randomResult = Random.Range (0, 100);
		CharacterPO po;
		if (randomResult < 30) {
			// 30% 生成特定人物
			po = CharacterGenerator.getInstance().createOneSpecialCharacter();
		}else{
			// 70% 生成随机人物
			po = CharacterGenerator.getInstance().createOneRandomCharacter();
		}
		m_textPlayerName.text = po.name;
		m_textPlayerSta.text = po.attrSTA.ToString();
		m_textPlayerAgi.text = po.attrAGI.ToString();
		m_textPlayerInt.text = po.attrINT.ToString();
		m_textPlayerSpr.text = po.attrSPR.ToString();
		m_textPlayerVit.text = po.attrVIT.ToString();
		m_textPlayerLuc.text = po.attrLUC.ToString();
		m_imgCharacter.sprite = ResourceManager.getInstance ().getSprite ("Sprite/Character/"+po.img);
		m_imgJob.sprite = ResourceManager.getInstance ().getSprite ("Sprite/Job/"+po.job);
	}
}

