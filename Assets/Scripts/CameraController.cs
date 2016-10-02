using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject m_target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (m_target) {
			Vector3 pPosition = transform.position;
			pPosition.x = m_target.transform.position.x;
			pPosition.z = m_target.transform.position.z;
			transform.position = pPosition;
		}
	}
}
