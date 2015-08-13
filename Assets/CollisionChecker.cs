using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionChecker : MonoBehaviour {

	#region serialized field
	[SerializeField]
	private GameObject m_collisionCheckerBit;
	
	[SerializeField]
	private GameObject m_landingPoint;

	[SerializeField]
	private float m_power = 500f;

	[SerializeField]
	private float m_mass = 1.0f;
	#endregion

	#region private variables
	private GameObject m_landingPointInstance;
	#endregion

	private List<Vector3> m_positions = new List<Vector3>();
	private int m_countLimit = 10;


	// Use this for initialization
	void Start () {
		m_landingPointInstance = Instantiate(m_landingPoint, transform.position, Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.frameCount % 2 == 0) {
			return;
		}

		Vector3 force            = transform.forward * m_power;
		GameObject checkerObj    = Instantiate(m_collisionCheckerBit, transform.position, Quaternion.identity) as GameObject;
		CollisionCheckerBit checkerBit = checkerObj.GetComponent<CollisionCheckerBit>();
		checkerBit.IgnoreObjects.Add(gameObject);
		checkerBit.IgnoreObjects.Add(m_landingPointInstance);
		
		checkerBit.OnDetect += (Vector3? point) => {
			Detected(point);
		};
		
		checkerBit.CheckStart(m_mass, checkerObj.transform.position, force, Physics.gravity);
	}
	
	/// <summary>
	/// Detected collision at point.
	/// </summary>
	/// <param name="point">Point.</param>
	void Detected(Vector3? point) {
		if (point == null) {
			return;
		}

		m_positions.Add(point ?? Vector3.zero);
		if (m_positions.Count > m_countLimit) {
			m_positions.RemoveAt(0);
		}
		Vector3 pos = Vector3.zero;
		foreach(Vector3 p in m_positions) {
			pos += p;
		}
		pos /= m_positions.Count;
		m_landingPointInstance.transform.position = pos;
	}
}
