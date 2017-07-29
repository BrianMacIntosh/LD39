using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDamager : MonoBehaviour
{
	private static List<GhostDamager> s_allDamagers = new List<GhostDamager>();

	[SerializeField]
	private float m_radius = 1f;

	[SerializeField]
	private float m_arc = 90f;

	private void Awake()
	{
		s_allDamagers.Add(this);
	}

#if UNITY_EDITOR
	[UnityEditor.Callbacks.DidReloadScripts]
#endif
	private static void DidReloadScripts()
	{
		if (s_allDamagers == null)
		{
			s_allDamagers = new List<GhostDamager>();
		}
		s_allDamagers.AddRange(FindObjectsOfType<GhostDamager>());
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		if (m_arc >= 360f)
		{
			Gizmos.DrawWireSphere(transform.position, m_radius);
		}
	}

	/// <summary>
	/// Returns true if the specified position is inside this light.
	/// </summary>
	public bool Contains(Vector3 position)
	{
		Transform transform = this.transform;
		float angle = Mathf.Atan2(position.y - transform.position.y, position.x - transform.position.x);
		float dx = position.x - transform.position.x;
		float dy = position.y - transform.position.y;
		float distanceSq = (dx * dx) + (dy * dy);

		return distanceSq < (m_radius * m_radius);
	}

	/// <summary>
	/// Returns true if the specified position is inside any <see cref="GhostDamager"/>.
	/// </summary>
	public static bool AnyContains(Vector3 position)
	{
		foreach (GhostDamager damager in s_allDamagers)
		{
			if (damager.Contains(position))
			{
				return true;
			}
		}
		return false;
	}
}
