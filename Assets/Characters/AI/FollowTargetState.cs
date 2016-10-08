using UnityEngine;
using System.Collections;

public class FollowTargetState : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	void OnEnable () {
		Debug.Log ("Subscribing for target events with object:", gameObject);
		PerceptionController.DoTargetChangedEvent += OnTargetChangedEvent;
	}

	void OnDisable () {
		PerceptionController.DoTargetChangedEvent -= OnTargetChangedEvent;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTargetChangedEvent (GameObject i_gameObject, GameObject i_target) {
		if (i_gameObject == gameObject) {
			Debug.Log ("Target changed");
		}
	}
}
