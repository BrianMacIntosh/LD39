using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInNearPlayer : MonoBehaviour
{
	[SerializeField]
	private float m_range = 3f;

	private float m_alpha = 0f;

	#region Cached Components

	private SpriteRenderer m_renderer;

	#endregion

	private void Awake()
	{
		m_renderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		float minRangeSq = float.MaxValue;
		foreach (GameObject gameObject in GameManager.Instance.Players)
		{
			minRangeSq = Mathf.Min(minRangeSq, (gameObject.transform.position - transform.position).sqrMagnitude);
		}
		if (minRangeSq <= m_range * m_range)
		{
			m_alpha = Mathf.Clamp01(m_alpha + Time.deltaTime);
		}
		else
		{
			m_alpha = Mathf.Clamp01(m_alpha - Time.deltaTime);
		}
		Color color = m_renderer.color;
		color.a = m_alpha;
		m_renderer.color = color;
	}
}
