using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHealth : MonoBehaviour
{
	[Tooltip("The time it takes for one damager to kill the ghost.")]
	[SerializeField]
	private float m_timeToKill = 2f;

	[Tooltip("The time it takes for the ghost to regenerate to full.")]
	[SerializeField]
	private float m_timeToRegen = 0.5f;

	/// <summary>
	/// The ghost's current health.
	/// </summary>
	private float m_health = 1f;

	/// <summary>
	/// True if the ghost is currently being damaged.
	/// </summary>
	public bool IsBeingDamaged { get; private set; }

	void Update()
	{
		float hitCount = GhostDamager.DamageAmount(transform.position);
		if (hitCount > 0f)
		{
			IsBeingDamaged = true;

			m_health -= Time.deltaTime * hitCount / m_timeToKill;
			if (m_health <= 0f)
			{
				Destroy(gameObject);
			}
		}
		else
		{
			IsBeingDamaged = false;

			m_health = Mathf.Clamp01(m_health + Time.deltaTime / m_timeToRegen);
		}
	}
}
