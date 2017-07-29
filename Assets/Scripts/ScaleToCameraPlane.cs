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

	private void OnValidate()
	{
		UpdateScale();
	}

	private void Update()
	{
		if (m_lastScreenWidth != m_camera.pixelWidth
			|| m_lastScreenHeight != m_camera.pixelHeight)
		{
			m_lastScreenWidth = m_camera.pixelWidth;
			m_lastScreenHeight = m_camera.pixelHeight;
			UpdateScale();
		}
	}

	private void UpdateScale()
	{
		if (m_camera.orthographic)
		{
			transform.localScale = new Vector3(
				m_camera.orthographicSize * m_camera.pixelWidth / m_camera.pixelHeight,
				transform.localScale.y,
				m_camera.orthographicSize) * ManualScale;
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
