using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererOverrides : MonoBehaviour
{
	private Renderer m_renderer;

	[SerializeField]
	private bool m_receiveShadows = false;

	[SerializeField]
	private int m_orderInLayer = 0;

	[SerializeField]
	private string m_sortLayer = "";

	private void OnValidate()
	{
		if (m_renderer == null)
		{
			m_renderer = GetComponent<Renderer>();
		}
		m_renderer.receiveShadows = m_receiveShadows;
		if (!string.IsNullOrEmpty(m_sortLayer))
		{
			m_renderer.sortingLayerName = m_sortLayer;
			m_renderer.sortingOrder = m_orderInLayer;
		}
	}
}
