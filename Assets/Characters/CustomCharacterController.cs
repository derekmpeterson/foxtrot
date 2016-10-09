using UnityEngine;
using System.Collections;

public class CustomCharacterController : MonoBehaviour {

	public float m_moveSpeed = 1.0f;


	private Rigidbody m_rigidbody;

	public static EventSystem.SimpleGameEvent ActionAttackEvent = new EventSystem.SimpleGameEvent();

	// Use this for initialization
	void Start () {
		m_rigidbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Move(Vector3 i_direction, float i_rate = 1.0f) {
		Vector3 pMovement = i_direction* i_rate * m_moveSpeed * Time.deltaTime;
		m_rigidbody.MovePosition (m_rigidbody.position + pMovement);
	}

	public void LookAtPoint(Vector3 i_targetPoint) {
		Vector3 pDirection = i_targetPoint - transform.position;
		pDirection.Normalize ();
		Look (pDirection);
	}

	public void Look(Vector3 i_direction) {
		Quaternion pTargetRotation = Quaternion.LookRotation (i_direction);
		transform.rotation = Quaternion.Slerp (transform.rotation, pTargetRotation, Mathf.PI * 4.0f * Time.deltaTime);
	}

	public static void TriggerActionAttackEvent(GameObject i_object) {
		ActionAttackEvent.Invoke (i_object);
	}
}
