using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JCFramework;
using System.IO;

namespace JCGame{
	public class CharacterGenerator : JCSingleton<CharacterGenerator> {
		private bool printLog = true;

		List<string> male_name_dict;
		List<string> female_name_dict;
		List<string> lastname_dict;

		private CharacterGenerator(){
			male_name_dict = new List<string> ();
			female_name_dict = new List<string> ();
			lastname_dict = new List<string> ();

			StreamReader malenameSR = new StreamReader (Application.dataPath+"/Resources/Data/malename.txt");
			string line = malenameSR.ReadLine ();
			while (line != null) {
				male_name_dict.Add (line);
				line = malenameSR.ReadLine ();
			}
			malenameSR.Close ();

			StreamReader femalenameSR = new StreamReader (Application.dataPath+"/Resources/Data/femalename.txt");
			line = femalenameSR.ReadLine ();
			while (line != null) {
				female_name_dict.Add (line);
				line = femalenameSR.ReadLine ();
			}
			femalenameSR.Close ();

			StreamReader lastnameSR = new StreamReader (Application.dataPath+"/Resources/Data/malename.txt");
			line = lastnameSR.ReadLine ();
			while (line != null) {
				lastname_dict.Add (line);
				line = lastnameSR.ReadLine ();
			}
			lastnameSR.Close ();
		}

		string getRandomName(){
			bool isMale = Random.Range (0, 100) < 50;
			string firstname = "";
			if (isMale) {
				firstname = male_name_dict [Random.Range (0, male_name_dict.Count)];
			} else {
				firstname = male_name_dict [Random.Range (0, female_name_dict.Count)];
			}
			string lastname = lastname_dict [Random.Range (0, lastname_dict.Count)];
			return firstname + " " + lastname;
		}

		public CharacterPO createOneSpecialCharacter(){
			CharacterPO charPO = JsonDB.getInstance ().create<CharacterPO> ();
			CharacterBaseData randomBaseData = CharacterBaseInfoHelper.getInstance ().getRandomBaseInfo ();
//			charPO.id = 0;
			charPO.img = "character"+Random.Range (1, 19);
			charPO.exp = 0;
			charPO.name = randomBaseData.character_name;
			charPO.dataId = randomBaseData.character_id;
			charPO.job = randomBaseData.character_job;
			charPO.attrSTA = randomBaseData.init_sta;
			charPO.attrAGI = randomBaseData.init_agi;
			charPO.attrINT = randomBaseData.init_int;
			charPO.attrSPR = randomBaseData.init_spr;
			charPO.attrVIT = randomBaseData.init_vit;
			charPO.attrLUC = randomBaseData.int_luc;

			return charPO;
		}
		public CharacterPO createOneRandomCharacter(){
			CharacterPO charPO = JsonDB.getInstance ().create<CharacterPO> ();
			RandomCharacterBaseData randomBaseData = RandomCharacterBaseInfoHelper.getInstance ().getRandomBaseInfo ();
//			charPO.id = 0;
			charPO.img = "character"+Random.Range (1, 19);
			charPO.exp = 0;
			charPO.name = getRandomName ();
			charPO.dataId = randomBaseData.tplt_id;
			charPO.job = randomBaseData.job;
			charPO.attrSTA = Random.Range (randomBaseData.min_sta, randomBaseData.max_sta + 1);
			charPO.attrAGI = Random.Range (randomBaseData.min_agi, randomBaseData.max_agi + 1);
			charPO.attrINT = Random.Range (randomBaseData.min_int, randomBaseData.max_int + 1);
			charPO.attrSPR = Random.Range (randomBaseData.min_spr, randomBaseData.max_spr + 1);
			charPO.attrVIT = Random.Range (randomBaseData.min_vit, randomBaseData.max_vit + 1);
			charPO.attrLUC = Random.Range (randomBaseData.min_luc, randomBaseData.max_luc + 1);

			return charPO;
		}
	}
}
