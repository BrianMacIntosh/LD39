using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSensor : MonoBehaviour
{
	[Tooltip("The door to control from this sensor.")]
	[SerializeField]
	private Door m_target = null;

	private void OnDisable()
	{
		m_target.RemoveActiveSensorAll(this);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		m_target.AddActiveSensor(this);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		m_target.RemoveActiveSensor(this);
	}
}
