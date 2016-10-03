using UnityEngine;
using System.Collections;

public class MunitionController : MonoBehaviour {

	public float m_damage;
	public float m_speed = 1.0f;

	GameObject m_owner;
	Rigidbody m_rigidbody;

	// Use this for initialization
	void Start () {
		m_rigidbody = GetComponent<Rigidbody> ();
	}

	public void SetOwner (GameObject i_owner) {
		m_owner = i_owner;
	}
	
	// Update is called once per frame
	void Update () {
		m_rigidbody.MovePosition(transform.position + (transform.forward * m_speed * Time.deltaTime));
	}

	void OnTriggerEnter (Collider i_collider) {
		if (i_collider.gameObject != m_owner) {
			HealthController.SendDamageEvent (i_collider.gameObject, gameObject, m_damage);
			Destroy (gameObject);
		}
	}

	public static void CreateMunition (GameObject i_object, GameObject i_owner, Transform i_transform) {
		GameObject pMunition = (GameObject)Instantiate (i_object);
		pMunition.transform.position = i_transform.position;
		pMunition.transform.rotation = i_transform.rotation;
		MunitionController pMunitionController = pMunition.GetComponent<MunitionController> ();
		pMunitionController.SetOwner (i_owner);
	}
}
