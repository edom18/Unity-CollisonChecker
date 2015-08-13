using UnityEngine;
using System.Collections;

public class FPS : MonoBehaviour {
	
	private int m_frame = 0;
	private float m_time = 0;
	private int m_FPS = 0;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		m_frame++;
		m_time += Time.deltaTime;
		
		if (m_time >= 1.0) {
			m_FPS = m_frame;
			m_frame = 0;
			m_time = 0;
		}
	}
	
	void OnGUI() {
		GUI.TextArea(new Rect(10, 10, 150, 20), "FPS: " + m_FPS);
	}
}