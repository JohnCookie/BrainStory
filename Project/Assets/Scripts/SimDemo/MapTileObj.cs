using UnityEngine;
using System.Collections;

public class MapTileObj : MonoBehaviour
{
	public UISprite m_spriteTileBg;

	string[] tileNameArr = {"ground8", "ground6", "ground3", "ground1", "ground4"};

	public void Init(int type){
		Debug.Log (type);
		m_spriteTileBg.spriteName = tileNameArr [type];
		if (type == 1) {
			gameObject.layer=8;
//			gameObject.layer = LayerMask.NameToLayer("Ground");
//			for(int i=0; i<transform.childCount; i++){
//				transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Ground");
//			}
		} else {
			gameObject.layer=9;
//			gameObject.layer = LayerMask.NameToLayer("Obstacle");
//			for(int i=0; i<transform.childCount; i++){
//				transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Obstacle");
//			}
		}
	}
	
	public void Clicked(){
		//player.tryMoveTo (transform.position);
		Debug.Log("tile clicked");
		if (SimMap.m_alies.Count > 0) {
			SimMap.m_alies [0].tryMoveTo (transform.position);
			Debug.Log("aaaa->"+transform.position);
		}
	}
}

