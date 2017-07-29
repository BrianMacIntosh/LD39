using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	/// <summary>
	/// List of sensors that are currently active. Can have duplicates.
	/// </summary>
	private List<DoorSensor> m_activeSensors = new List<DoorSensor>();

	#region Cached Components

	private Animator m_animator = null;

	#endregion

	private static int s_animatorOpenParam;

	[RuntimeInitializeOnLoadMethod]
#if UNITY_EDITOR
	[UnityEditor.Callbacks.DidReloadScripts]
#endif
	private static void StaticInit()
	{
		s_animatorOpenParam = Animator.StringToHash("Open");
	}

	/// <summary>
	/// Notifies the door that the specified sensor has been activated.
	/// </summary>
	public void AddActiveSensor(DoorSensor sensor)
	{
		m_activeSensors.Add(sensor);
		UpdateOpenState();
	}

	/// <summary>
	/// Notifies the door that the specified sensor has been deactivated.
	/// </summary>
	public void RemoveActiveSensor(DoorSensor sensor)
	{
		m_activeSensors.Remove(sensor);
		UpdateOpenState();
	}

	/// <summary>
	/// Notifies the door that the specified sensor has been deleted.
	/// </summary>
	public void RemoveActiveSensorAll(DoorSensor sensor)
	{
		for (int i = m_activeSensors.Count - 1; i >= 0;i--)
		{
			if (m_activeSensors[i] == sensor) m_activeSensors.RemoveAt(i);
		}
		UpdateOpenState();
	}

	private void UpdateOpenState()
	{
		if (m_animator == null)
		{
			m_animator = GetComponent<Animator>();
		}
		if (m_animator != null)
		{
			m_animator.SetBool(s_animatorOpenParam, m_activeSensors.Count > 0);
		}
	}
}
