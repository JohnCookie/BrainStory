using UnityEngine;
using System.Collections;

public class TaskPageUI : MonoBehaviour
{
	GameObject taskItemObj;
	UIGrid taskGrid;

	void Awake(){
		taskItemObj = transform.FindChild ("taskPageBg/offset/TaskScroll/TaskItem").gameObject;
		taskGrid = transform.FindChild ("taskPageBg/offset/TaskScroll/TaskGrid").GetComponent<UIGrid> ();
	}

	// Use this for initialization
	void Start ()
	{
		TestGenerateTasks ();
	}

	void TestGenerateTasks(){
		taskItemObj.SetActive (true);

		int childNum = taskGrid.transform.childCount;
		for (int i=0; i<childNum; i++) {
			GameObject.Destroy(taskGrid.transform.GetChild(i).gameObject);
		}
		taskGrid.transform.DetachChildren ();

		for (int i=0; i<8; i++) {
			GameObject rawItem = Instantiate(taskItemObj) as GameObject;
			rawItem.transform.parent = taskGrid.transform;
			rawItem.transform.localScale = Vector3.one;
			rawItem.transform.localPosition = Vector3.zero;
		}

		taskGrid.Reposition ();

		taskItemObj.SetActive (false);
	}
}

