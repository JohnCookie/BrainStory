using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class TaskSystemMgr : MonoBehaviour
{
	private static TaskSystemMgr _instance;
	private TaskSystemMgr(){
		
	}
	public static TaskSystemMgr getInstance(){
		if (_instance == null) {
			_instance=GameObject.Find ("TaskSystemMgr").gameObject.GetComponent<TaskSystemMgr> ();
			_instance.Init();		
		}
		return _instance;
	}
	
	void Init(){
		// read user task data from saving
		UserTasksList mData = JsonMapper.ToObject<UserTasksList>(TextUtils.getInstance ().ReadTextFromResources ("SavingData"));
		m_listUserTasks.Clear ();
		for (int i=0; i<mData.data.Count; i++) {
			UserTaskInfo t = mData.data[i];
			m_listUserTasks.Add(t);
		}

		UserTaskBaseList mData2 = JsonMapper.ToObject<UserTaskBaseList>("SavingData2");
		m_listUserAllTasks.Clear ();
		for (int i=0; i<mData2.data.Count; i++) {
			UserTaskBaseInfo t = mData2.data[i];
			m_listUserAllTasks.Add(t);
		}
		Debug.Log("TaskSystemMgr Inited");
	}

	List<UserTaskInfo> m_listUserTasks = new List<UserTaskInfo> ();
	List<UserTaskBaseInfo> m_listUserAllTasks = new List<UserTaskBaseInfo> ();

	// tasks player is doing
	public List<UserTaskInfo> getUserProcessingTaskList(){
		return m_listUserTasks;
	}

	// tasks in player list
	public List<UserTaskBaseInfo> getUserAllTaskList(){
		return m_listUserAllTasks;
	}

	// get num for one rarity in player list
	public int getPlayerTaskNumByRarity(int rarity){
		int num = 0;
		for (int i=0; i<m_listUserAllTasks.Count; i++) {
			if(rarity == TaskDataUtility.getInstance().getTaskBaseDataById(m_listUserAllTasks[i].id).task_rarity){
				num++;
			}
		}
		return num;
	}

	// get task num now
	int getPlayerTaskNum(){
		return m_listUserAllTasks.Count;
	}

	// check task already in list
	bool checkTaskInList(int id){
		bool result = false;
		for(int i=0; i<m_listUserAllTasks.Count; i++){
			if(m_listUserAllTasks[i].id == id){
				result = true;
				break;
			}
		}
		return result;
	}

	// random a task not in list
	TaskData randomATask(){
		List<TaskData> taskList = TaskDataUtility.getInstance ().getAllTaskList ();
		int taskId = UnityEngine.Random.Range (1, taskList.Count);
		while(checkTaskInList(taskId)){
			taskId = UnityEngine.Random.Range (1, taskList.Count);
		}
		return TaskDataUtility.getInstance().getTaskBaseDataById(taskId);
	}
}

