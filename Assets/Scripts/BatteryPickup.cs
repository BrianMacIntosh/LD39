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
}
