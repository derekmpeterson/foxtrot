using UnityEngine;
using System.Collections;

public class FollowTargetState : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	void OnEnable () {
		PerceptionController.TargetChangedEvent.AddListener (OnTargetChangedEvent);
	}

	void OnDisable () {
		PerceptionController.TargetChangedEvent.RemoveListener (OnTargetChangedEvent);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTargetChangedEvent (GameObject i_gameObject, EventSystem.EventData i_data) {
		if (i_gameObject == gameObject) {
			Debug.Log ("Target changed");
		}
	}
}
