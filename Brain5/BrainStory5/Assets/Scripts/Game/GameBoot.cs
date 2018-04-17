using UnityEngine;
using System.Collections;
using JCFramework;

/// <summary>
/// 游戏入口
/// </summary>
public class GameBoot : MonoBehaviour
{
	void Awake(){
		
	}

	void Start(){
		// 开始加载文本数据
		loadGameData();
	}

	void Update(){
		
	}

	void Destroy(){
		
	}

	void loadGameData(){
		LogManager.getInstance().Log(CardBaseInfoHelper.getInstance ().getCardBaseInfo (1).card_name, LogLevel.Information);
		LogManager.getInstance().Log(CharacterBaseInfoHelper.getInstance ().getCharacterBaseInfo (1).character_name, LogLevel.Information);
	}
}

