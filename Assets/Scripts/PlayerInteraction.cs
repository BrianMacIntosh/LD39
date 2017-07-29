using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	[SerializeField]
	private PlayerType m_playerType;

	[SerializeField]
	private string m_interactControl;

	[SerializeField]
	private float m_interactRadius = 1f;

	[SerializeField]
	[Range(0f, 360f)]
	private float m_interactArc = 180f;

	/// <summary>
	/// The pickup that the player is currently holding.
	/// </summary>
	private Pickup m_holdingPickup = null;

	private void OnDestroy()
	{
		SetDownPickup();
	}

	private void Update()
	{
		if (Input.GetButtonDown(m_interactControl))
		{
			if (m_holdingPickup != null)
			{
				SetDownPickup();
			}
			else
			{
				PickUpPickup();
			}
		}
	}

	private void SetDownPickup()
	{
		if (m_holdingPickup != null)
		{
			m_holdingPickup.transform.SetParent(null);
			m_holdingPickup.HeldBy = null;
			m_holdingPickup = null;
		}
	}

	private void PickUpPickup()
	{
		Pickup target = GetPickupToGrab(m_playerType, transform.position, transform.forward);
		if (target != null)
		{
			target.transform.SetParent(transform);
			m_holdingPickup = target;
		}
	}

	/// <summary>
	/// Returns the pickup that can be grabbed by this player, if any.
	/// </summary>
	private Pickup GetPickupToGrab(PlayerType playerType, Vector3 position, Vector3 forward)
	{
		float bestPickupRating = float.MaxValue;
		Pickup bestPickup = null;

		foreach (Pickup pickup in Pickup.Pickups)
		{
			if (pickup != null
				&& pickup.HeldBy == null
				&& pickup.CanBeGrabbedBy(playerType))
			{
				Vector2 d = pickup.transform.position - position;

				if (d.sqrMagnitude <= m_interactRadius * m_interactRadius)
				{
					float angle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
					float dAng = Mathf.Abs(Mathf.DeltaAngle(angle - 90f, transform.rotation.eulerAngles.z));

					if (dAng < m_interactArc / 2f)
					{
						// this pickup is in range
						float rating = d.magnitude + dAng / 180f;
						if (rating < bestPickupRating)
						{
							bestPickupRating = rating;
							bestPickup = pickup;
						}
					}
				}
			}
		}

		return bestPickup;
	}
}
