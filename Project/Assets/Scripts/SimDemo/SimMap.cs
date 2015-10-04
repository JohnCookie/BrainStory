using UnityEngine;
using System.Collections;
using System;

public class SimMap : MonoBehaviour
{
	public const int tileWidth = 32;
	public const int tileHeight = 32;
	public const int mapWidthNum = 50;
	public const int mapHeightNum = 50;

	public int[,] currSimMapArray = new int[50,50];
	bool mapInitialed = false;

	public GameObject m_objTileMap;
	public UIGrid m_mapGrid;

	void Awake(){
		LoadMapText ();
		if (!mapInitialed) {
			GenerateMapView ();
		} else {

		}
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
}

