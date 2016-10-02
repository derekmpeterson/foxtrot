﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	CustomCharacterController m_characterController;

	// Use this for initialization
	void Start () {
		m_characterController = GetComponent<CustomCharacterController> ();
	}

	// Update is called once per frame
	void Update () {
		HandleInput ();
		LookAtMouse ();
	}

	void HandleInput () {
		Vector3 pDirection = Vector3.zero;
		pDirection += Vector3.forward * Input.GetAxis ("Vertical");
		pDirection += Vector3.right * Input.GetAxis ("Horizontal");

		if (pDirection.sqrMagnitude > 0.0f) {
			m_characterController.Move (pDirection.normalized, pDirection.magnitude);
		}
	}

	void LookAtMouse () {
		Plane pPlayerPlane = new Plane(Vector3.up, transform.position);
		Ray pRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		float pHitDist = 0.0f;
		if (pPlayerPlane.Raycast (pRay, out pHitDist)) {
			Vector3 pTargetPoint = pRay.GetPoint (pHitDist);
			m_characterController.LookAtPoint (pTargetPoint);
		}
	}
}