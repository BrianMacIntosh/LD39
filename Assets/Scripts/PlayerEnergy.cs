using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
	public float currentEnergy = 100;
	public float maxEnergy = 100;
	public float drainRate = 0.25f;
	public int ghostsAttacking = 0;
	public float ghostDrainRate = 5;

	[SerializeField]
	private Animator m_lightAnimator = null;

	[SerializeField]
	private AudioClip m_outOfPowerAudio = null;

	[SerializeField]
	private Sprite m_outOfPowerSprite = null;

	[SerializeField]
	private Sprite m_lightOnSprite = null;

	[SerializeField]
	private Sprite m_lightOffSprite = null;

	public bool HasEnergy { get { return currentEnergy > 0; } }

	public float MissingEnergy { get { return maxEnergy - currentEnergy; } }

	[SerializeField]
	private AudioSource m_audioSource;

	#region Cached Components

	private SpriteRenderer m_spriteRenderer;

	#endregion

	private void Awake()
	{
		m_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		AddEnergy(-drainRate * Time.deltaTime);
		AddEnergy(-ghostsAttacking * ghostDrainRate * Time.deltaTime);
	}

	void OnRespawn()
	{
		AddEnergy(maxEnergy);
	}

	public void AddEnergy(float amount)
	{
		float previousEnergy = currentEnergy;
		currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, maxEnergy);

		m_lightAnimator.SetFloat("Energy", currentEnergy / maxEnergy);

		if (previousEnergy > 0f && currentEnergy <= 0f)
		{
			m_audioSource.PlayOneShot(m_outOfPowerAudio);
		}

		UpdateSprite();
	}

	private void UpdateSprite()
	{
		if (currentEnergy <= 0f)
		{
			m_spriteRenderer.sprite = m_outOfPowerSprite;
		}
		else
		{
			m_spriteRenderer.sprite = m_lightOnSprite;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ghost")
		{
			ghostsAttacking++;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ghost")
		{
			ghostsAttacking--;
		}
	}
}
