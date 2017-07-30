using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	public static CameraControl Instance
	{
		get
		{
			if (s_instance == null)
			{
				s_instance = FindObjectOfType<CameraControl>();
			}
			return s_instance;
		}
	}
	private static CameraControl s_instance = null;

	private Vector2 m_targetPosition;

	private void Update()
	{
		Vector3 newPosition = Vector2.Lerp(transform.position, m_targetPosition, Time.deltaTime * 5f);
		newPosition.z = transform.position.z;
		transform.position = newPosition;
	}

	public void GoTo(Vector2 position)
	{
		m_targetPosition = position;
	}
}
