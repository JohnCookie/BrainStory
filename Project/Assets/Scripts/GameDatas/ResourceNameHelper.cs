using UnityEngine;
using System.Collections;

public class ResourceNameHelper
{
	const string ROUND_FRAME_SUFFIX = "_circle_frame";
	const string SQUARE_FRAME_SUFFIX = "_block_frame";

	private static ResourceNameHelper _instance;
	private ResourceNameHelper(){

	}

	public static ResourceNameHelper getInstance(){
		if(_instance==null){
			_instance = new ResourceNameHelper();
		}
		return _instance;
	}

	string getNameByQuality(int _quality){
		string _n = "grey";
		switch(_quality){
		case 1:
			_n = "grey";
			break;
		case 2:
			_n = "green";
			break;
		case 3:
			_n = "blue";
			break;
		case 4:
			_n = "purple";
			break;
		case 5:
			_n = "orange";
			break;
		default:
			_n = "grey";
			break;
		}
		return _n;
	}

	public string getRoundFrameNameByQuality(int _quality){
		return getNameByQuality(_quality)+ROUND_FRAME_SUFFIX;
	}

	public string getSquareFrameNameByQuality(int _quality){
		return getNameByQuality(_quality)+SQUARE_FRAME_SUFFIX;
	}

	public string getAttackTypeName(MonsterAtkType type){
		string _n = "atk_sharp";
		switch(type){
		case MonsterAtkType.Sharp:
			_n = "atk_sharp";
			break;
		case MonsterAtkType.Blunt:
			_n = "atk_dun";
			break;
		case MonsterAtkType.Arrow:
			_n = "atk_arrow";
			break;
		case MonsterAtkType.Magic:
			_n = "atk_magic";
			break;
		default:
			_n = "atk_sharp";
			break;
		}
		return _n;
	}

	public string getDefenceTypeName(MonsterDefType type){
		string _n = "armor_no";
		switch(type){
		case MonsterDefType.NoArmor:
			_n = "armor_no";
			break;
		case MonsterDefType.LightArmor:
			_n = "armor_light";
			break;
		case MonsterDefType.HeavyArmor:
			_n = "armor_heavy";
			break;
		case MonsterDefType.EnhancedArmor:
			_n = "armor_enhance";
			break;
		default:
			_n = "armor_no";
			break;
		}
		return _n;
	}
}

