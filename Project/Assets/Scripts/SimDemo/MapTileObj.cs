using UnityEngine;
using System.Collections;

public class MapTileObj : MonoBehaviour
{
	public UISprite m_spriteTileBg;
	public enum TileBgType{
		Empty,
		Street,
		Ground,
		House
	}
	string[] tileNameArr = {"ground8", "ground6", "ground3", "ground4", "ground5"};

	public void Init(int type){
		Debug.Log (type);
		m_spriteTileBg.spriteName = tileNameArr [type];
	}
}

