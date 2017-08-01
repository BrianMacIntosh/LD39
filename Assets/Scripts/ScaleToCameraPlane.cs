using UnityEngine;

/// <summary>
/// Scales the object to fill the entire plane of the main camera at its depth.
/// </summary>
[ExecuteInEditMode]
public class ScaleToCameraPlane : MonoBehaviour
{
	[SerializeField]
	private Camera m_camera = null;

	private int m_lastScreenWidth = 0;
	private int m_lastScreenHeight = 0;

	private Vector3[] m_corners = new Vector3[4];

	public float ManualScale = 0.1f;

	public bool DisableX;
	public bool DisableY;
	public bool FlipYZ;

	private void OnValidate()
	{
		UpdateScale();
	}

	private void Update()
	{
		if (m_lastScreenWidth != Screen.width
			|| m_lastScreenHeight != Screen.height)
		{
			m_lastScreenWidth = Screen.width;
			m_lastScreenHeight = Screen.height;
			UpdateScale();
		}
	}

	public void UpdateScale()
	{
		if (m_camera.orthographic)
		{
			transform.localScale = new Vector3(
				(m_camera.orthographicSize * Screen.width / Screen.height) * ManualScale,
				FlipYZ ? m_camera.orthographicSize * ManualScale : transform.localScale.y,
				FlipYZ ? transform.localScale.z : m_camera.orthographicSize * ManualScale);
		}
		else
		{
			m_camera.CalculateFrustumCorners(
				new Rect(0f, 0f, 1f, 1f),
				m_camera.transform.position.z - transform.position.z,
				Camera.MonoOrStereoscopicEye.Mono,
				m_corners);
			transform.localScale = new Vector3(
				DisableX ? transform.localScale.x : m_corners[1].x - m_corners[2].x,
				transform.localScale.y,
				DisableY ? transform.localScale.z : m_corners[0].y - m_corners[1].y) * ManualScale;
		}
	}
}
