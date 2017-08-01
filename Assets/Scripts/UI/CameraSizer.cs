using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fixes the problem where the UI runs horizontally off the screen by letterboxing.
/// </summary>
public class CameraSizer : MonoBehaviour
{
	private Camera m_camera;

	private float m_originalOrthoSize;

	private const float TargetAspect = 1920f / 1080f;

	private int m_lastScreenWidth = 0;
	private int m_lastScreenHeight = 0;

	private void Awake()
	{
		m_camera = GetComponent<Camera>();
		m_originalOrthoSize = m_camera.orthographicSize;
	}

	void Update()
	{
		// if the aspect ratio is narrower than the target ratio, increase the ortho size to ensure
		// the UI remains on the screen
		if (m_lastScreenWidth != Screen.width
			|| m_lastScreenHeight != Screen.height)
		{
			float currentAspect = Screen.width  / (float)Screen.height;
			float requiredWidth = m_originalOrthoSize * TargetAspect;
			float requiredHeight = requiredWidth / TargetAspect;

			m_camera.orthographicSize = (9f * TargetAspect) / Screen.width * Screen.height;

			m_lastScreenWidth = Screen.width;
			m_lastScreenHeight = Screen.height;

			foreach (ScaleToCameraPlane planeScale in GetComponentsInChildren<ScaleToCameraPlane>())
			{
				planeScale.UpdateScale();
			}
		}
	}
}
