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

	[SerializeField]
	private AudioClip m_moveStartAudio = null;

	[SerializeField]
	private AudioClip m_moveLoopAudio = null;

	[SerializeField]
	private AudioClip m_moveEndAudio = null;

	private float m_lastMoveForward = 0f;
	private float m_lastTurn = 0f;

	//TODO: change to enum
    public bool isBeep = true;

	private float m_angularVelocity = 0f;

	[SerializeField]
	private AudioSource m_audioSource;

	#region Cached Components

	private PlayerEnergy m_energyComponent;
	private Rigidbody2D m_rigidbody;
	private Animator m_animator;

	#endregion

	private void Awake()
	{
		m_energyComponent = GetComponent<PlayerEnergy>();
		m_rigidbody = GetComponent<Rigidbody2D>();
		m_animator = GetComponent<Animator>();
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
		
		if (moveForward != 0f || turn != 0f)
		{
			if (m_lastMoveForward == 0f && m_lastTurn == 0f)
			{
				m_audioSource.clip = m_moveStartAudio;
				m_audioSource.loop = false;
				m_audioSource.volume = 0.1f;
				m_audioSource.Play();
			}
			else if (!m_audioSource.isPlaying)
			{
				m_audioSource.clip = m_moveLoopAudio;
				m_audioSource.loop = true;
				m_audioSource.volume = 0.1f;
				m_audioSource.Play();
			}
		}
		else if (m_lastMoveForward != 0f || m_lastTurn != 0f
			&& Mathf.Abs(moveForward) != 1f || Mathf.Abs(turn) != 1f
			&& m_audioSource.clip != m_moveEndAudio)
		{
			m_audioSource.clip = m_moveEndAudio;
			m_audioSource.loop = false;
			m_audioSource.volume = 0.1f;
			m_audioSource.Play();
		}

		m_animator.SetBool("IsMoving", moveForward != 0f || turn != 0f);
		m_animator.speed = Mathf.Max(1f, m_rigidbody.velocity.magnitude / 1.25f);

		m_lastMoveForward = moveForward;
		m_lastTurn = turn;
	}

    private Vector2 GetVector2FromAngle(float degAngle)
    {
        float radAngle = degAngle * Mathf.PI / 180;
        return new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle));
    }
}
