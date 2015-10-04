using UnityEngine;
using System.Collections;

public class TileMapManager : MonoBehaviour
{
	private static TileMapManager _instance;

	private TileMapManager(){

	}

	public static TileMapManager getInstance(){
		if (_instance == null) {
			_instance = new TileMapManager();		
		}
		return _instance;
	}
}

