using UnityEngine;
using System.Collections;

public class MainPageUI : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
			Debug.Log("This is MainPageUI start");
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void GoToNextPage(){
			Debug.Log("click show test page");
			UISystem.getInstance().showPage("Prefabs/TestPageUI");
		}
}

