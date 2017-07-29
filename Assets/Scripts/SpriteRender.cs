using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRender : MonoBehaviour
{
	private Renderer m_renderer;

	[SerializeField]
	private bool m_receiveShadows = false;

	private void OnValidate()
	{
		if (m_renderer == null)
		{
			m_renderer = GetComponent<Renderer>();
		}
		m_renderer.receiveShadows = m_receiveShadows;
	}
}
