using UnityEngine;
using System.Collections;
using Pathfinding;

public class PathFinding2DTest : MonoBehaviour {

	public GameObject targetFlag;
	Vector3 target;
	public Camera m_camera;
	public UIRoot root;
	RaycastHit hitt = new RaycastHit();
	
	private Seeker seeker;
	
	public Path path;
	
	public float nextWayPointDistance = 0.1f;
	private int currWayPoint = 0;
	public float speed = 5;
	
	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker> ();
		
		//seeker.StartPath (transform.position, target.position, OnPathCompleted);
	}
	
	// Update is called once per frame
	void Update () {
		if (path == null) {
			return;	
		}

		if (currWayPoint >= path.vectorPath.Count) {
			Debug.Log("End path Reached");
			path = null;
			targetFlag.SetActive(false);
			return;
		}
		
		Vector3 dir = (path.vectorPath[currWayPoint]-transform.position).normalized;
		dir *= speed * Time.deltaTime;
		transform.localPosition += dir;
		//Check if we are close enough to the next waypoint
		//If we are, proceed to follow the next waypoint
		if (Vector3.Distance (transform.position,path.vectorPath[currWayPoint]) < nextWayPointDistance) {
			currWayPoint++;
			return;
		}
	}
	
	public void OnPathCompleted(Path p){
		Debug.Log("Yay, we got a path back. Did it have an error? "+p.error);
		if (!p.error) {
			path = p;
			currWayPoint = 0;	
		}
	}

	public void tryMoveTo (Vector3 targetPostion)
	{
		targetFlag.transform.localPosition = targetPostion / root.transform.localScale.x;
		targetFlag.SetActive (true);
		target = targetPostion;
		seeker.StartPath (transform.position, target, OnPathCompleted);
	}
}
