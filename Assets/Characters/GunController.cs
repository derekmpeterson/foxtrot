using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

	public GameObject m_bullet;
	public float m_fireRate = 0.25f;

	private float m_fireTick = 0.0f;

	// Use this for initialization
	void Start () {
		
	}

	void OnEnable () {
		CustomCharacterController.DoActionAttackEvent += OnActionAttackEvent;
	}

	void OnDisable () {
		CustomCharacterController.DoActionAttackEvent -= OnActionAttackEvent;
	}
	
	// Update is called once per frame
	void Update () {
		m_fireTick = Mathf.Clamp (m_fireTick - Time.deltaTime, 0.0f, m_fireRate);
	}

	public void Fire () {
		if (m_fireTick <= 0.0f) {
			Transform pGun = transform.Find ("Gun").Find("Muzzle");
			MunitionController.CreateMunition (m_bullet, gameObject, pGun);

			m_fireTick = m_fireRate;
		}
	}

	public void OnActionAttackEvent (GameObject i_object) {
		if (i_object == gameObject) {
			Fire ();
		}
	}
}
