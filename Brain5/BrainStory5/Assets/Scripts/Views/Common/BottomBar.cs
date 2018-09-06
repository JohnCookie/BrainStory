using UnityEngine;
using System.Collections;
using JCFramework;

public class BottomBar : MonoBehaviour
{
	public void onMission(){
	}

	public void onTeam(){
		ViewManager.getInstance ().ShowView ("CharacterView");
	}

	public void onBattle(){
		
	}

	public void onFarm(){
	
	}

	public void onResearch(){
	
	}
}

