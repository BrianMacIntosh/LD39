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

	public bool HasEnergy { get { return currentEnergy > 0; } }

	void Update()
	{
		AddEnergy(-drainRate * Time.deltaTime);
		AddEnergy(-ghostsAttacking * ghostDrainRate * Time.deltaTime);
	}

	public void AddEnergy(float amount)
	{
		currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, maxEnergy);

		m_lightAnimator.SetFloat("Energy", currentEnergy / maxEnergy);
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
