using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMove : MonoBehaviour
{
    private GameObject beep;
    public float pushSpeed = 6;
    public bool isPushed = false;

	private Rigidbody2D m_rigidbody;

	private void Awake()
	{
		m_rigidbody = GetComponent<Rigidbody2D>();
	}

	void Start()
    {
		GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
        if ((playerList[0]).GetComponent<Player_Navigation>().isBeep)
        {
            beep = playerList[0];
        }
        else
        {
            beep = playerList[1];
        }
    }
	
	public void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject == beep)
		{
			m_rigidbody.AddForce(collision.contacts[0].normal * pushSpeed * m_rigidbody.mass, ForceMode2D.Force);
		}
	}
}
