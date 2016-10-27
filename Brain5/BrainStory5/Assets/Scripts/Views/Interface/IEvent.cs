using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IEvent
{
	List<int> EventList{ get;}
	void handleEvent (int eventId, params object[] paramList); 
}

