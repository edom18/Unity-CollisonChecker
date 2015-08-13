using UnityEngine;
using System.Collections;

public class ChangeDirection : MonoBehaviour {

	private float m_angle = 0;

	// Update is called once per frame
	void Update () {
		m_angle += 0.05f;
		float angle = 180f * Mathf.Sin(m_angle) / 5f;

		Vector3 rot = transform.rotation.eulerAngles;
		rot.x = angle;
		transform.eulerAngles = rot;
	}
}
