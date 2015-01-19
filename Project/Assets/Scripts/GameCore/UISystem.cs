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

public class PageObjectRef{
	int _index;
	string _pageName;
	GameObject m_objPage;

	public PageObjectRef(int index, string name, GameObject obj){
		this._index = index;
		this._pageName = name;
		this.m_objPage = obj;
	}

	public int Index{
		get{
			return _index;
		}
		set{
			_index=value;
		}
	}

	public string Name{
		get{
			return _pageName;
		}
		set{
			_pageName=value;
		}
	}

	public GameObject Page{
		get{
			return m_objPage;
		}
	}
}

public class UISystem: MonoBehaviour {
	private static UISystem _instance;

	public Dictionary<string, PageObjectRef> mPageDict = new Dictionary<string, PageObjectRef>();
	public List<PageObjectRef> mPageList = new List<PageObjectRef> ();
	public string mCurrPageName = "";
	public string mLastPageName = "";

	GameObject m_mainPanel;
	Camera m_mainCamera;

	private UISystem(){
	}

	public void Init(){
		mPageList.Clear ();
		mPageDict.Clear ();

		GameObject mainPage = GameObject.Find ("MainPageUI").gameObject;
		PageObjectRef tmpRef = new PageObjectRef (0, "MainPageUI", mainPage);
		mPageList.Add (tmpRef);
		mPageDict.Add ("MainPageUI", tmpRef);
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
		GameObject targetPage=null;
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
				targetPage = obj as GameObject;
				targetPage.name = getNameFromPath(pageName);
				targetPage.transform.parent = MainPanel.transform;
				targetPage.transform.localPosition = getPageLocation(PagePositions.CommonPage);
				targetPage.transform.localScale = Vector3.one;

				mLastPageName = mCurrPageName;
				mCurrPageName = pageName;

				PageObjectRef tmpObj = new PageObjectRef(mPageList.Count, pageName, targetPage);
				mPageDict.Add(pageName, tmpObj);
				mPageList.Add(tmpObj);
			});
		}

		if (callback != null) {
			callback(targetPage);		
		}
	}

	public void showMainPage(){

	}

	public void showLastPage(){
		GameObject lastPage = mPageDict [mLastPageName].Page;
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

	private int getStoreddPageIndex(string pageName){
		return 0;
	}
}
