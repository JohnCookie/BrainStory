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

	public int[,] currSimMapArray = new int[30,30];
	bool mapInitialed = false;

	public GameObject m_objTileMap;
	public UIGrid m_mapGrid;
	public GameObject m_objNpc;
	public GameObject m_nodeNpcs;

	public static List<AutoWalkingObj> m_alies = new List<AutoWalkingObj> ();

	void Awake(){
		LoadMapText ();
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
				Debug.Log(currSimMapArray[x, y]);
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

		GenerateNPC ();
	}

	public void GenerateNPC(){
		GameObject npc = Instantiate (m_objNpc) as GameObject;
		npc.transform.parent = m_nodeNpcs.transform;
		npc.transform.localPosition = new Vector3(-16,-16,0);
		npc.transform.localScale = Vector3.one;
		npc.name = "npc";
		AutoWalkingObj npcScript = npc.GetComponent<AutoWalkingObj> ();
		m_alies.Add (npcScript);

		m_objNpc.SetActive (false);
	}
}

