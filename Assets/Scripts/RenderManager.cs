using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderManager : MonoBehaviour
{
	private RenderTexture m_lightRenderTexture = null;

	[SerializeField]
	private Material m_lightPassMaterial = null;

	private Camera m_camera;

	private void Awake()
	{
		m_camera = GetComponent<Camera>();
		CreateRenderTexture();
	}

	private void OnPreRender()
	{
		if (m_lightRenderTexture.width != Screen.width
			|| m_lightRenderTexture.height != Screen.height)
		{
			CreateRenderTexture();
		}
	}

	private void CreateRenderTexture()
	{
		if (m_lightRenderTexture)
		{
			Destroy(m_lightRenderTexture);
		}
		m_lightRenderTexture = new RenderTexture(Screen.width, Screen.height, 0);
		m_lightPassMaterial.mainTexture = m_lightRenderTexture;
		m_camera.targetTexture = m_lightRenderTexture;
	}
}
