using UnityEngine;
using System.Collections;
using Pathfinding;

public class EnemyController : MonoBehaviour {

	const float c_pathTickRate = 0.25f;
	const float c_waypointDistanceAllowance = 0.1f;

	CustomCharacterController m_characterController;
	PerceptionController m_perceptionController;
	Seeker m_seeker;

	Path m_currentPath;
	int m_currentWaypoint = 0;
	float m_pathTick = 0.0f;

	// Use this for initialization
	void Start () {
		m_characterController = GetComponent<CustomCharacterController> ();
		m_perceptionController = GetComponent<PerceptionController> ();
		m_seeker = GetComponent<Seeker> ();
	}
	
	// Update is called once per frame
	void Update () {
//		GameObject pTarget = m_perceptionController.GetTarget ();
//		if (pTarget && m_pathTick <= 0.0f) {// && m_characterController.CanSeeTarget()) {
//			m_pathTick = c_pathTickRate;
//			m_seeker.StartPath (transform.position, pTarget.transform.position, OnPathComplete);
//		} else if (!pTarget) {
//			m_currentPath = null;
//			m_currentWaypoint = 0;
//		}
//
//		m_pathTick -= Time.deltaTime;
//
//		if (m_currentPath != null) {
//			if (m_currentWaypoint < m_currentPath.vectorPath.Count) {
//				Vector3 pDir = (m_currentPath.vectorPath [m_currentWaypoint] - transform.position).normalized;
//				if (pDir.sqrMagnitude > 0.0f) {
//					m_characterController.Look (pDir);
//					m_characterController.Move (pDir);
//				}
//
//				if (Vector3.Distance (transform.position, m_currentPath.vectorPath [m_currentWaypoint]) < c_waypointDistanceAllowance) {
//					m_currentWaypoint++;
//				}
//			}
//		}
	}

	public void OnPathComplete (Path i_path) {
		if (!i_path.error) {
			m_currentPath = i_path;
		}
	}
}
