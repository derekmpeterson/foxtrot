using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

	const float c_healthRegenTickRate = 0.5f;

	public float m_maxHealth = 100.0f;
	public float m_regenRate = 0.1f;

	private float m_health = 100.0f;
	private float m_healthRegenTick = 0.0f;


	public delegate void DamageEvent(GameObject i_victim, GameObject i_attacker, float i_damage);
	public static event DamageEvent DoDamageEvent;
	public delegate void DeathEvent(GameObject i_gameObject);
	public static event DeathEvent DoDeathEvent;
	public delegate void KillEvent(GameObject i_attacker);
	public static event KillEvent DoKillEvent;

	// Use this for initialization
	void Start () {
		m_health = m_maxHealth;
	}

	void OnEnable () {
		HealthController.DoDamageEvent += OnDamageEvent;
	}

	void OnDisable () {
		HealthController.DoDamageEvent -= OnDamageEvent;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_healthRegenTick <= 0.0f) {
			if (m_health < m_maxHealth) {
				float pDelta = m_maxHealth * m_regenRate * c_healthRegenTickRate;
				ChangeHealth (pDelta);
			}
			m_healthRegenTick += c_healthRegenTickRate;
		}
		m_healthRegenTick -= Time.deltaTime;

		if (m_health <= 0.0f) {
			// death
			Destroy(gameObject);
			return;
		}
	}

	public void ChangeHealth (float i_healthDelta, GameObject i_attacker = null) {
		m_health = Mathf.Clamp (m_health + i_healthDelta, 0.0f, m_maxHealth);

		if (m_health <= 0.0f) {
			Death (i_attacker);
		}
	}

	public void Death (GameObject i_attacker) {
		if (DoDeathEvent != null) {
			DoDeathEvent(gameObject);
		}

		if (DoKillEvent != null) {
			DoKillEvent(i_attacker);
		}
	}

	public void OnDamageEvent(GameObject i_victim, GameObject i_attacker, float i_damage) {
		if (i_victim == gameObject) {
			ChangeHealth (-i_damage, i_attacker);
		}
	}

	public static void SendDamageEvent(GameObject i_victim, GameObject i_attacker, float i_damage) {
		if (DoDamageEvent != null) {
			DoDamageEvent(i_victim, i_attacker, i_damage);
		}
	}
}
