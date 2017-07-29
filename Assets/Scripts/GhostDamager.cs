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

	private void OnDestroy()
	{
		s_allDamagers.Remove(this);
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
		else
		{
			Vector3 cornerA = Quaternion.AngleAxis(-m_arc / 2f, Vector3.forward) * transform.forward * m_radius + transform.position;
			Vector3 cornerB = Quaternion.AngleAxis(m_arc / 2f, Vector3.forward) * transform.forward * m_radius + transform.position;
			Gizmos.DrawLine(transform.position, cornerA);
			Gizmos.DrawLine(transform.position, cornerB);
		}
	}

	/// <summary>
	/// Returns true if the specified position is inside this light.
	/// </summary>
	public bool Contains(Vector3 position)
	{
		Transform transform = this.transform;
		Vector2 d = position - transform.position;
		float distanceSq = (d.x * d.x) + (d.y * d.y);

		// check distance
		if (distanceSq < (m_radius * m_radius))
		{
			// check arc
			float angle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
			float dAng = Mathf.Abs(Mathf.DeltaAngle(angle - 90f, transform.parent.rotation.eulerAngles.z)); //HACK: parent
			if (dAng < m_arc / 2f)
			{
				// check los
				if (!Physics2D.Raycast(transform.position, d.normalized, Mathf.Sqrt(distanceSq), LayerMask.GetMask("Walls")))
				{
					return true;
				}
			}
		}

		return false;
	}

	/// <summary>
	/// Returns the number of <see cref="GhostDamager"/>s that the point is inside of.
	/// </summary>
	public static int ContainsCount(Vector3 position)
	{
		int count = 0;
		foreach (GhostDamager damager in s_allDamagers)
		{
			if (damager != null && damager.Contains(position))
			{
				count++;
			}
		}
		return count;
	}
}
