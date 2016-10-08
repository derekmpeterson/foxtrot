using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AITree : MonoBehaviour {

	public Stack<string> m_states;

	private MonoBehaviour m_currentState;

	// Use this for initialization
	void Start () {
		m_states = new Stack<string> ();


		PushState ("FollowTargetState");
	}
	
	// Update is called once per frame
	void Update () {
		if (m_states.Count > 0) {
			string pStateName = m_states.Peek ();
			if (m_currentState == null) {
				// TODO: AOT / stripping issue?
				//	https://blogs.unity3d.com/2015/01/21/addcomponentstring-api-removal-in-unity-5-0/
				System.Type pType = System.Type.GetType (pStateName);
				MonoBehaviour pState = gameObject.AddComponent (pType) as MonoBehaviour;
				m_currentState = pState;
			}
		}
	}

	public void PushState(string i_stateName) {
		m_states.Push(i_stateName);
	}

	public void PopState() {
		m_states.Pop ();
		Destroy (m_currentState);
		m_currentState = null;
	}
}
