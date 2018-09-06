using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using JCFramework;

public class CharacterController:JCSingleton<CharacterController>{
	List<CharacterPO> allCharacterPOList;
	Dictionary<int, CharacterPO> allCharacterPODict;

	private CharacterController(){
		allCharacterPOList = new List<CharacterPO> ();
		allCharacterPODict = new Dictionary<int, CharacterPO> ();
	}

	public void Init(){
		allCharacterPOList.Clear ();
		allCharacterPOList = JsonDB.getInstance ().getAll<CharacterPO> ();
		allCharacterPODict.Clear ();
		for (int i = 0; i < allCharacterPOList.Count; i++) {
			if (allCharacterPODict.ContainsKey (allCharacterPOList [i].id)) {
				LogManager.getInstance ().Log ("duplicate po id: " + allCharacterPOList [i].id + " in character po", LogLevel.Error);
			} else {
				allCharacterPODict.Add (allCharacterPOList [i].id, allCharacterPOList [i]);
			}
		}
	}

	public List<CharacterPO> AllCharacterList{
		get { return allCharacterPOList; }
	}
}
		