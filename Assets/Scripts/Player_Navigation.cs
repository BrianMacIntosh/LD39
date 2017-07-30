using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
	None,
	Beep,
	Boop,
	Both,
}

public class Player_Navigation : MonoBehaviour {

    public float move_speed = 5;
    public float turn_speed = 100;
    public float dirDegAngle = 90;

	//TODO: change to enum
    public bool isBeep = true;

	private float m_angularVelocity = 0f;

	#region Cached Components

	private PlayerEnergy m_energyComponent;
	private Rigidbody2D m_rigidbody;

	#endregion

	private void Awake()
	{
		m_energyComponent = GetComponent<PlayerEnergy>();
		m_rigidbody = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		float turn = 0f;
		float moveForward = 0f;
		if (m_energyComponent.HasEnergy)
		{
			if (isBeep)
			{
				turn = Input.GetAxis("BeepHorizontal");
				moveForward = Input.GetAxis("BeepVertical");
			}
			else
			{
				turn = Input.GetAxis("BoopHorizontal");
				moveForward = Input.GetAxis("BoopVertical");
			}
		}

		Vector2 direction = GetVector2FromAngle(dirDegAngle);
		Vector2 moveDirection = new Vector2(direction[0] * moveForward, direction[1] * moveForward);

		m_rigidbody.velocity = moveDirection * move_speed;
		m_angularVelocity = -turn * turn_speed;
		dirDegAngle = transform.rotation.eulerAngles.z + 90;

		m_rigidbody.rotation += m_angularVelocity * Time.deltaTime;
	}

    private Vector2 GetVector2FromAngle(float degAngle)
    {
        float radAngle = degAngle * Mathf.PI / 180;
        return new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle));
    }
}
