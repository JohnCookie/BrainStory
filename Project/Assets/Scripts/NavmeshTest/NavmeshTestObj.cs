using UnityEngine;
using System.Collections;

public class NavmeshTestObj : MonoBehaviour {

	NavMeshAgent agent;
	Vector3 target;
	public Camera m_camera;
	RaycastHit hitt = new RaycastHit();

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			Ray ray = m_camera.ScreenPointToRay(Input.mousePosition);
			Physics.Raycast(ray, out hitt);
			if (null != hitt.transform)
			{
				if(hitt.transform.name.Equals("Plane")){
					target = hitt.point;
				}
			}
			
			agent.SetDestination (target);
		}
	}
}
