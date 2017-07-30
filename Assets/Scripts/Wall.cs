using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
	[SerializeField]
	private int m_width = 1;

	[SerializeField]
	private int m_height = 1;

	private void OnValidate()
	{
		BoxCollider2D collider = GetComponent<BoxCollider2D>();
		SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		GameObject losBlocker = GetComponentInChildren<MeshRenderer>().gameObject;

		collider.size = new Vector2(m_width, m_height);
		foreach (SpriteRenderer spriteRenderer in spriteRenderers)
		{
			spriteRenderer.size = collider.size;
		}
		losBlocker.transform.localScale = new Vector3(m_width, m_height, losBlocker.transform.localScale.z);
	}
}
