using UnityEngine;
using System.Collections;
using Pathfinding;

public class PathFindingTest : MonoBehaviour {

	public Transform target;

	private Seeker seeker;
	private CharacterController controller;

	public Path path;

	public float speed = 100f;

	public float nextWayPointDistance = 3;
	private int currWayPoint = 0;


	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker> ();
		controller = GetComponent<CharacterController> ();

		seeker.StartPath (transform.position, target.position, OnPathCompleted);
	}
	
	// Update is called once per frame
	void Update () {
		if (path == null) {
			return;	
		}

		if (currWayPoint >= path.vectorPath.Count) {
			Debug.Log("End path Reached");
			path = null;
			return;
		}

		Vector3 dir = (path.vectorPath[currWayPoint]-transform.position).normalized;
		dir *= speed * Time.deltaTime;
		controller.SimpleMove (dir);
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
}
