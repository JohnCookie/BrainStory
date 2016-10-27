using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace JCFramework{
	public class EventManager : JCSingleton<EventManager>
	{
		public Dictionary<int, List<IEvent>> eventList;

		private EventManager(){
			eventList = new Dictionary<int, List<IEvent>> ();
		}

		public void registEvents(IEvent events){
			for (int i = 0; i < events.EventList.Count; i++) {
				int eventId = events.EventList [i];
				if (eventList.ContainsKey (eventId)) {
					eventList [eventId].Add (events);
				} else {
					List<IEvent> receiverList = new List<IEvent> ();
					receiverList.Add (events);
					eventList.Add (eventId, receiverList);
				}
			}
		}

		public void unregistEvents(IEvent events){
			for (int i = 0; i < events.EventList.Count; i++) {
				int eventId = events.EventList [i];
				eventList [eventId].Remove (events);
			}
		}

		public void dispatchEvent(int eventId, params object[] paramList){
			for (int i = 0; i < eventList [eventId].Count; i++) {
				eventList [eventId] [i].handleEvent (eventId, paramList);
			}
		}
	}
}

