using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMove : MonoBehaviour
{
    public float pushSpeed = 6;
    public bool isPushed = false;

	private Rigidbody2D m_rigidbody;

	private void Awake()
	{
		m_rigidbody = GetComponent<Rigidbody2D>();
	}
	
	public void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject == GameManager.Instance.Beep)
		{
			m_rigidbody.AddForce(collision.contacts[0].normal * pushSpeed * m_rigidbody.mass, ForceMode2D.Force);
		}
	}
}
