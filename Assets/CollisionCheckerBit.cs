using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class CollisionCheckerBit : MonoBehaviour {
	
	#region events
	public delegate void DetectHandler(Vector3? point);
	public event DetectHandler OnDetect;
	#endregion
	
	#region private variables
	private Rigidbody m_rigidbody;
	private Vector3 m_previousPosition;
	#endregion

	#region serialized field
	[SerializeField]
	private int m_detectLimit = 1000;
	#endregion
	
	#region public variables
	public List<GameObject> IgnoreObjects;
	#endregion

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		//
	}
	
	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake() {
		m_rigidbody = GetComponent<Rigidbody>();
		IgnoreObjects.Add(gameObject);
	}

	/// <summary>
	/// Checks start.
	/// </summary>
	/// <param name="mass">Mass.</param>
	/// <param name="startPosition">Start position.</param>
	/// <param name="force">Force.</param>
	/// <param name="gravity">Gravity.</param>
	public void CheckStart(float mass, Vector3 startPosition, Vector3 force, Vector3 gravity) {
		float time      = 0;
		float interval  = Time.fixedDeltaTime;
		int frame      = 0;

		Vector3 speed;
		m_previousPosition = CalcPositionFromForce(0, mass, startPosition, force, gravity, out speed);
		while (frame < m_detectLimit) {
			Vector3 pos = CalcPositionFromForce(time, mass, startPosition, force, gravity, out speed);
			m_rigidbody.MovePosition(pos);

			Vector3 direction = pos - m_previousPosition;
			m_previousPosition = pos;

			float distance = speed.sqrMagnitude * interval;
			RaycastHit hit;
			bool needIgnore = false;
			if (m_rigidbody.SweepTest(direction, out hit, distance)) {
				foreach (GameObject ignore in IgnoreObjects) {
					if (hit.collider.gameObject == ignore) {
						needIgnore = true;
						break;
					}
				}
					
				// Continue the loop if collider is in ignore objects.
				if (needIgnore) {
					time += interval;
					frame++;
					continue;
				}

				if (OnDetect != null) {
					OnDetect(hit.point);
				}

				Destroy(gameObject);
				return;
			}
			
			time += interval;
			frame++;
		}
		
		if (OnDetect != null) {
			OnDetect(null);
		}
		
		Destroy(gameObject);
	}
	
	/// <summary>
	/// Calculates the position from force.
	/// </summary>
	/// <returns>The position from force.</returns>
	/// <param name="time">Time.</param>
	/// <param name="mass">Mass.</param>
	/// <param name="startPosition">Start position.</param>
	/// <param name="force">Force.</param>
	/// <param name="gravity">Gravity.</param>
	Vector3 CalcPositionFromForce(float time, float mass, Vector3 startPosition, Vector3 force, Vector3 gravity, out Vector3 speed) {
		speed = (force / mass) * Time.fixedDeltaTime;
		Vector3 position = (speed * time) + (gravity * 0.5f * Mathf.Pow(time, 2));
		
		return startPosition + position;
	}
}
