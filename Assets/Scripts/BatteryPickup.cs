using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : Pickup
{
	[Tooltip("The amount of energy the pickup restores.")]
	[SerializeField]
	private float m_energy = 1f;

	public override void NotifyPickUp(PlayerInteraction pickUpper)
	{
		base.NotifyPickUp(pickUpper);

		pickUpper.GetComponent<PlayerEnergy>().AddEnergy(m_energy);

		Destroy(gameObject);
	}

	public override bool CanBeGrabbedBy(GameObject player)
	{
		if (player.GetComponent<PlayerEnergy>().MissingEnergy < m_energy)
		{
			return false;
		}
		else
		{
			return base.CanBeGrabbedBy(player);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player")
			&& CanBeGrabbedBy(collision.gameObject))
		{
			//HACK: this is just about the worst code I have ever written
			PlayerInteraction pickUpper = collision.gameObject.GetComponent<PlayerInteraction>();
			pickUpper.GetComponent<PlayerEnergy>().AddEnergy(m_energy);
			Destroy(gameObject);
			if (m_onPickUpAudio)
			{
				AudioSource audioSource = OurUtility.GetOrAddComponent<AudioSource>(pickUpper.gameObject);
				audioSource.PlayOneShot(m_onPickUpAudio);
			}
		}
	}
}
