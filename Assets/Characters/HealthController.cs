using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

	const float c_healthRegenTickRate = 0.5f;

	public float m_maxHealth = 100.0f;
	public float m_regenRate = 0.1f;

	private float m_health = 100.0f;
	private float m_healthRegenTick = 0.0f;

	// Use this for initialization
	void Start () {
		m_health = m_maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_healthRegenTick <= 0.0f) {
			if (m_health < m_maxHealth) {
				float pDelta = m_maxHealth * m_regenRate * c_healthRegenTickRate;
				m_health = Mathf.Clamp (m_health + pDelta, 0.0f, m_maxHealth);
			}
			m_healthRegenTick += c_healthRegenTickRate;
		}
		m_healthRegenTick -= Time.deltaTime;
	}
}
