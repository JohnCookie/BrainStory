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
		// 初始化数据库
		initDB ();
		// 开始加载文本数据 游戏基础表
		loadGameData();
		// 读取玩家存档数据
		loadUserData();
		// 显示主界面
		showMainView ();
	}

	void Update(){
		
	}

	void Destroy(){
		
	}

	void initDB(){
		JsonDB.getInstance ().createTable<CharacterPO> ();
	}

	void loadGameData(){
		LogManager.getInstance().Log(CardBaseInfoHelper.getInstance ().getCardBaseInfo (1).card_name, LogLevel.Information);
		LogManager.getInstance().Log(CharacterBaseInfoHelper.getInstance ().getCharacterBaseInfo (1001).character_name, LogLevel.Information);
	}

	void loadUserData(){
		CharacterController.getInstance ().Init ();
	}

	void showMainView(){
		ViewManager.getInstance ().ShowView ("StatsView");
	}
}

