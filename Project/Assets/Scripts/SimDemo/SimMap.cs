using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SimMap : MonoBehaviour
{
	public const int tileWidth = 32;
	public const int tileHeight = 32;
	public const int mapWidthNum = 30;
	public const int mapHeightNum = 30;

	public int[,] currSimMapArray = new int[30, 30];
	public int[,] currSimMapInfoArray = new int[30, 30];
	Dictionary<int, List<string>> mapInfoDict = new Dictionary<int, List<string>>();
	bool mapInitialed = false;
	public bool gridAstarFinished = false;

	public GameObject m_objTileMap;
	public UIGrid m_mapGrid;
	public GameObject m_objNpc;
	public GameObject m_nodeNpcs;

	public static List<AutoWalkingObj> m_alies = new List<AutoWalkingObj> ();

	void Awake(){
		LoadMapText ();
		LoadMapInfo ();
		if (!mapInitialed) {
			GenerateMapView ();
		} else {

		}
		TimerHelper.getInstance().DelayFunc(1, regenerateGrid);
	}

	void LoadMapText(){
		string mapRes = TextUtils.getInstance ().ReadTextFromResources ("Texts/GameData/map_text");
		mapRes = mapRes.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty).Replace(" ", string.Empty);
		if (mapRes.Length > mapWidthNum * mapHeightNum) {
			mapRes.Substring (0, mapWidthNum * mapHeightNum);		
		} else if (mapRes.Length < mapWidthNum * mapHeightNum) {
			for (int i=mapRes.Length; i<= mapWidthNum * mapHeightNum; i++) {
				mapRes += "0";
			}
		} else {
			Debug.Log("map file fine");
		}
		char[] mapChar = mapRes.ToCharArray ();

		for(int x=0; x<mapWidthNum; x++){
			for(int y=0; y<mapHeightNum; y++){
				currSimMapArray[x, y] = int.Parse(mapChar[x*mapWidthNum+y].ToString());
			}
		}
	}

	void LoadMapInfo(){
		string mapRes = TextUtils.getInstance().ReadTextFromResources("Texts/GameData/map_info");
		mapRes = mapRes.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("\t", string.Empty).Replace(" ", string.Empty);
		if (mapRes.Length > mapWidthNum * mapHeightNum) {
			mapRes.Substring (0, mapWidthNum * mapHeightNum);		
		} else if (mapRes.Length < mapWidthNum * mapHeightNum) {
			for (int i=mapRes.Length; i<= mapWidthNum * mapHeightNum; i++) {
				mapRes += "0";
			}
		} else {
			Debug.Log("map file fine");
		}
		char[] mapChar = mapRes.ToCharArray ();
		mapInfoDict.Clear ();

		for(int x=0; x<mapWidthNum; x++){
			for(int y=0; y<mapHeightNum; y++){
				currSimMapInfoArray[x, y] = int.Parse(mapChar[x*mapWidthNum+y].ToString());
				if(currSimMapInfoArray[x, y]>0){
					if(mapInfoDict.ContainsKey(currSimMapInfoArray[x, y])){
						List<string> currStr = mapInfoDict[currSimMapInfoArray[x, y]];
						currStr.Add(x+"_"+y);
					}else{
						List<string> str = new List<string>();
						str.Add(x+"_"+y);
						mapInfoDict.Add(currSimMapInfoArray[x, y], str);
					}
				}
			}
		}
	}

	void GenerateMapView(){

		for(int x=0; x<mapWidthNum; x++){
			for(int y=0; y<mapHeightNum; y++){
				GameObject obj = Instantiate(m_objTileMap) as GameObject;
				obj.transform.parent = m_mapGrid.transform;
				obj.transform.localPosition = Vector3.zero;
				obj.transform.localScale = Vector3.one;
				obj.name = x+"_"+y;
				MapTileObj mapTileScript = obj.transform.GetComponent<MapTileObj>();
				mapTileScript.Init(currSimMapArray[x,y]);
			}
		}
		m_mapGrid.Reposition ();

		m_objTileMap.SetActive (false);

		mapInitialed = true;
	}

	public void regenerateGrid(){
		AstarPath.active.Scan ();
		Debug.Log("rescan");

		//GenerateNPC ();
		m_objNpc.SetActive (false);
		gridAstarFinished = true;
	}

	public void GenerateNPC(){
		List<Vector3> npcPosAndTar = getRandomStartAndEndPoint ();

		GameObject npc = Instantiate (m_objNpc) as GameObject;
		npc.transform.parent = m_nodeNpcs.transform;
		npc.transform.localPosition = npcPosAndTar[0];
		npc.transform.localScale = Vector3.one;
		npc.name = "npc";
		npc.SetActive (true);
		AutoWalkingObj npcScript = npc.GetComponent<AutoWalkingObj> ();
		m_alies.Add (npcScript);
		npcScript.setEndPoint (npcPosAndTar [1]);
	}

	public List<Vector3> getRandomStartAndEndPoint(){
		int startIndex = UnityEngine.Random.Range (0, mapInfoDict [5].Count);
		string startPointStr = mapInfoDict [5] [startIndex];
		int endIndex = UnityEngine.Random.Range (0, mapInfoDict [5].Count);
		while(endIndex==startIndex){
			endIndex = UnityEngine.Random.Range (0, mapInfoDict [5].Count);
		}
		string endPointStr = mapInfoDict [5] [endIndex];
		List<Vector3> result = new List<Vector3> ();
		result.Add (strToVector (startPointStr));
		result.Add (strToVector (endPointStr));
		Debug.Log (result [0] + "-->" + result [1]);
		return result;
	}

	Vector3 strToVector(string pos){
		string[] pos_arr = pos.Split('_');
		int x = int.Parse (pos_arr [0]);
		int y = int.Parse (pos_arr [1]);
		return new Vector3 ((-mapWidthNum / 2 - 1) * tileWidth + tileWidth / 2 + x * tileWidth, 
		                    (mapHeightNum / 2 - 1) * tileHeight - tileHeight / 2 - y * tileHeight, 
		                    0);
	}
}

