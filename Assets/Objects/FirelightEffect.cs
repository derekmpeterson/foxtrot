using UnityEngine;
using System.Collections;

public class FirelightEffect : MonoBehaviour {

	const float c_maxReduction = 0.25f;
	const float c_maxIncrease = 0.25f;
	const float c_rateDamping = 0.1f;
	const float c_strength = 2.0f * Mathf.PI;

	Light m_light;
	float m_baseIntensity;
	private float m_flickerTick = 0.0f;

	// Use this for initialization
	void Start () {
		m_light = GetComponent<Light> ();
		m_baseIntensity = m_light.intensity;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_flickerTick <= 0.0f) {
			m_light.intensity = Mathf.Lerp (m_light.intensity, Random.Range (m_baseIntensity - c_maxReduction, m_baseIntensity + c_maxIncrease), c_strength * Time.deltaTime);
			m_flickerTick = c_rateDamping;
		}
		
		m_flickerTick -= Time.deltaTime;
	}
}
