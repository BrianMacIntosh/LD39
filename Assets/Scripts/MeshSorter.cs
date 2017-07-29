using UnityEngine;

public class MeshSorter : MonoBehaviour
{
	private MeshRenderer m_renderer;

	public int OrderInLayer
	{
		get { return m_renderer.sortingOrder; }
		set
		{
			m_orderInLayer = value;
			m_renderer.sortingOrder = value;
		}
	}
	[SerializeField]
	private int m_orderInLayer = 0;

	public string SortingLayer
	{
		get { return m_sortingLayer; }
		set
		{
			m_sortingLayer = value;
			m_renderer.sortingLayerName = value;
		}
	}
	[SerializeField]
	private string m_sortingLayer;

	private void OnValidate()
	{
		if (m_renderer == null)
		{
			m_renderer = GetComponent<MeshRenderer>();
		}
		m_renderer.sortingOrder = m_orderInLayer;
		m_renderer.sortingLayerName = m_sortingLayer;
	}
}
