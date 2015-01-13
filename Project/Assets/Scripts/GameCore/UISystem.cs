using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void ShowPageCallback (GameObject page);

public enum PagePositions{
	MainPage,
	CommonPage,
	CommonDialog,
	TopDialog,
	OutOfCamera
}

public class UISystem: MonoBehaviour {
	private static UISystem _instance;

	public Dictionary<string, GameObject> mPageDict = new Dictionary<string, GameObject>();
	public List<GameObject> mPageList = new List<GameObject> ();
	public string mCurrPageName = "";
	public string mLastPageName = "";

	GameObject m_mainPanel;
	Camera m_mainCamera;

	private UISystem(){
	}

	public static UISystem getInstance(){
		if (_instance == null) {
			_instance=new UISystem();		
		}
		return _instance;
	}


	public GameObject MainPanel {
		get {
			if (m_mainPanel == null) {
				m_mainPanel = GameObject.Find ("MainPanel").gameObject;
			}
			return m_mainPanel;
		}
	}

	public Camera MainCamera {
		get {
			if (m_mainCamera==null) {
				m_mainCamera = GameObject.Find("MainCamera").camera;
			}
			return m_mainCamera;
		}
	}

	public void showPage(string pageName){
		showPage (pageName, null);
	}
	public void showPage(string pageName, ShowPageCallback callback){
		if (isPageLoaded (pageName)) {
			Debug.LogError ("The Page Is Loaded Already");
			/*
			GameObject loadedPage = mPageDict[pageName];
			loadedPage.transform.parent = MainPanel.transform;
			loadedPage.transform.localPosition = PagePositions.CommonPage;
			loadedPage.transform.localScale = Vector3.one;
			*/
			return;	
		} else {
			ResourceSystem.getInstance().loadRes(pageName, delegate(Object obj) {
				GameObject targetPage = obj as GameObject;
				targetPage.name = getNameFromPath(pageName);
				targetPage.transform.parent = MainPanel.transform;
				targetPage.transform.localPosition = getPageLocation(PagePositions.CommonPage);
				targetPage.transform.localScale = Vector3.one;

				mLastPageName = mCurrPageName;
				mCurrPageName = pageName;
				mPageDict.Add(pageName, targetPage);
				mPageList.Add(targetPage);
			});
		}
	}

	public void showMainPage(){

	}

	public void showLastPage(){
		GameObject lastPage = mPageDict [mLastPageName];
		lastPage.transform.localPosition = getPageLocation(PagePositions.CommonPage);
		lastPage.transform.localScale = Vector3.one;

		mCurrPageName = lastPage.name;
	}

	private bool isPageLoaded(string pageName){
		if (mPageDict.ContainsKey (pageName)) {
			return true;
		}
		return false;
	}

	private string getNameFromPath(string path){
		string[] strArr = path.Split ('/');
		return strArr [strArr.Length - 1];
	}

	private Vector3 getPageLocation(PagePositions type){
		Vector3 v3;
		switch (type) {
		case PagePositions.MainPage:
			v3 = new Vector3(0, 0, -200);
			break;
		case PagePositions.CommonPage:
			v3 = new Vector3(0, 0, -500);
			break;
		case PagePositions.CommonDialog:
			v3 = new Vector3(0, 0, -800);
			break;
		case PagePositions.TopDialog:
			v3 = new Vector3(0, 0, -1000);
			break;
		case PagePositions.OutOfCamera:
			v3 = new Vector3(0, 0, 10000);
			break;
		default:
			v3 = new Vector3(0, 0, 0);
			break;
		}
		return v3;
	}
}
