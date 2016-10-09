using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PerceptionController : MonoBehaviour {

	const float c_targetingTickRate = 0.1f;

	public float m_fov = 45.0f;
	public float m_sightDistance = 6.0f;
	public LayerMask m_targetTypes;
	GameObject m_target;

	private float m_targetingTick = 0.0f;


	public static EventSystem.GameEvent TargetChangedEvent = new EventSystem.GameEvent ();
	public struct TargetChangedData : EventSystem.EventData {
		public GameObject m_target;
	};


	// Use this for initialization
	void Start () {
		
	}

	void OnEnable () {
		HealthController.DoDamageEvent += OnDamageEvent;
	}

	void OnDisable () {
		HealthController.DoDamageEvent -= OnDamageEvent;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_targetingTick <= 0.0f) {
			if (m_target == null) {
				GameObject pNearest = null;
				float pNearestDistanceSquared = float.MaxValue;
				List<GameObject> pMatches = FindMatches (m_targetTypes);
				for (int i = 0; i < pMatches.Count; i++) {
					GameObject pMatch = pMatches [i];
					float pDistanceSquared = (pMatch.transform.position - transform.position).sqrMagnitude;
					if (pDistanceSquared < pNearestDistanceSquared) {
						if (LineOfSight (pMatch)) {
							pNearest = pMatch;
							pNearestDistanceSquared = pDistanceSquared;
						}
					}
				}
				SetTarget (pNearest);
			} else {
				float pDistanceSquared = (m_target.transform.position - transform.position).sqrMagnitude;
				bool pLineOfSight = LineOfSight (m_target);
				if ((!pLineOfSight && pDistanceSquared > Mathf.Pow(m_sightDistance * 2.0f, 2.0f)) ||
					(pDistanceSquared > Mathf.Pow(m_sightDistance * 4.0f, 2.0f))) {
					// TODO: This should probably use pathLength, instead.
					SetTarget (null);
				}
			}
			m_targetingTick = c_targetingTickRate;
		}
		m_targetingTick -= Time.deltaTime;
	}

	bool LineOfSight (GameObject i_object) {
		Vector3 pTargetDirection = i_object.transform.position - transform.position;
		float pTargetDistanceSquared = pTargetDirection.sqrMagnitude;
		pTargetDirection.Normalize ();
		if (pTargetDistanceSquared <= Mathf.Pow(m_sightDistance, 2)/* || Vector3.Angle (pTargetDirection, transform.forward) <= m_fov*/) {
			RaycastHit pHit;
			Physics.Raycast (transform.position + Vector3.up * 0.5f, pTargetDirection, out pHit, 5.0f);
			if (pHit.collider && pHit.collider.gameObject == i_object) {
				Debug.DrawLine (transform.position + Vector3.up * 0.5f, pHit.point, Color.blue);
				return true;
			} else if (pHit.collider) {
				Debug.DrawLine (transform.position + Vector3.up * 0.5f, pHit.point, Color.green);
			} else {
				Debug.DrawLine (transform.position + Vector3.up * 0.5f, transform.position + (pTargetDirection * 5.0f) + Vector3.up * 0.5f, Color.red);
			}
		}
		return false;
	}

	public GameObject GetTarget () {
		return m_target;
	}

	public void SetTarget (GameObject i_target) {
		if (i_target != m_target) {
			TargetChangedData pData;
			pData.m_target = i_target;
			TargetChangedEvent.Invoke (gameObject, pData);
		}
		m_target = i_target;
	}

	List<GameObject> FindMatches (LayerMask i_findTypes) {
		List<GameObject> pMatches = new List<GameObject> ();
		Collider[] pColliders = Physics.OverlapSphere (transform.position, m_sightDistance);
		for (int i = 0; i < pColliders.Length; i++) {
			Collider pCollider = pColliders [i];
			if ((i_findTypes.value & 1 << pCollider.gameObject.layer) != 0) {
				pMatches.Add (pCollider.gameObject);
			}
		}
		return pMatches;
	}

	public void OnDamageEvent(GameObject i_victim, GameObject i_attacker, float i_damage) {
		if (i_victim == gameObject) {
			SetTarget (i_attacker);
		}
	}
}
