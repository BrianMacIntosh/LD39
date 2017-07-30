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
    private string m_interactTransferPower;

    [SerializeField]
	private float m_interactRadius = 1f;

	[SerializeField]
	[Range(0f, 360f)]
	private float m_interactArc = 180f;

	[SerializeField]
	private AudioClip m_extinguishFireAudio = null;

    public float m_powerTransferAmount = 20;

    public bool m_isBeep;
    public GameObject m_boop;
    public GameObject m_beep;
    //public GameObject m_fireWaterSpawnManager;
    //public GameObject m_objectiveSpawnManager;
	/// <summary>
	/// The pickup that the player is currently holding.
	/// </summary>
	public Pickup m_holdingPickup = null;
    private SpriteRenderer m_holdingPickupSprite;
    public SpriteRenderer m_waterSprite;
    public int m_objectiveCount;

	#region Cached Components

	private AudioSource m_audioSource;

	#endregion

	public delegate void objectSpawn(Vector3 position);

    public event objectSpawn onFireWaterPickup;
    public event objectSpawn onObjectivePickup;

    private void Start()
    {
		m_audioSource = OurUtility.GetOrAddComponent<AudioSource>(gameObject);

		m_objectiveCount = 0;
        m_isBeep = (m_playerType == PlayerType.Beep);
        bool beepIs1 = false;
        GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
        if ((playerList[0]).GetComponent<Player_Navigation>().isBeep)
        {
            beepIs1 = true;
        }
        if (beepIs1)
        {
            m_boop = playerList[1];
            m_beep = playerList[0];
        }
        else
        {
            m_boop = playerList[0];
            m_beep = playerList[1];
        }
    }

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
                TryGivePickupToBoop();
                TryPutOutFire();
                SetDownPickup();
			}
			else
			{
				PickUpPickup();
			}
		}
        if (Input.GetButtonDown(m_interactTransferPower))
        {
            TransferPower();
        }
	}

	public void SetDownPickup()
	{
		if (m_holdingPickup != null)
		{
            if (m_holdingPickup.CompareTag("Water"))
            {
                m_holdingPickupSprite.enabled = true;
                m_waterSprite.enabled = false;
            }
            m_holdingPickup.transform.SetParent(null);
			m_holdingPickup.NotifyDrop(this);
			m_holdingPickup = null;
		}
	}

    public bool TryGivePickupToBoop()
    {
        Vector2 d = m_boop.transform.position - transform.position;
        if (m_isBeep && m_holdingPickup.CompareTag("Objective") && (m_boop.GetComponent<PlayerInteraction>().m_objectiveCount < 5))
        {
            if (d.sqrMagnitude <= m_interactRadius * m_interactRadius)
            {
                float angle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
                float dAng = Mathf.Abs(Mathf.DeltaAngle(angle - 90f, transform.rotation.eulerAngles.z));

                if (dAng < m_interactArc / 2f)
                {
                    // Boop is in range
                    Destroy(m_holdingPickup.gameObject);
                    m_holdingPickup = null;
                    m_boop.GetComponent<PlayerInteraction>().m_objectiveCount++;
                    return true;
                }
            }
        }
        return false;
    }

    public bool TryPutOutFire()
    {
        if (m_holdingPickup.gameObject.CompareTag("Water"))
        {
            foreach (Pickup pickup in Pickup.Pickups)
            {
                if (pickup != null
                    && pickup.HeldBy == null)
                {
                    Vector2 d = pickup.transform.position - transform.position;

                    if (d.sqrMagnitude <= m_interactRadius * m_interactRadius)
                    {
                        float angle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
                        float dAng = Mathf.Abs(Mathf.DeltaAngle(angle - 90f, transform.rotation.eulerAngles.z));

                        if (dAng < m_interactArc / 2f)
                        {
                            // this pickup is in range
                            if (pickup.gameObject.CompareTag("Fire"))
                            {
                                if (onFireWaterPickup != null)
                                {
                                    onFireWaterPickup(pickup.transform.position);
                                }
                                Destroy(pickup.gameObject);
                                Destroy(m_holdingPickup.gameObject);
                                m_holdingPickup = null;
                                m_waterSprite.enabled = false;
								m_audioSource.PlayOneShot(m_extinguishFireAudio);
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }

	public void PickUpPickup()
	{
		Pickup target = GetPickupToGrab(m_playerType, transform.position, transform.forward);
		if (target != null)
		{
            if (target.CompareTag("Water"))
            {
                m_holdingPickupSprite = target.gameObject.GetComponent<SpriteRenderer>();
                m_holdingPickupSprite.enabled = false;
                m_waterSprite.enabled = true;
                if(onFireWaterPickup != null)
                {
                    onFireWaterPickup(target.transform.position);
                }
            }
            if(target.CompareTag("Objective"))
            {
                if (onObjectivePickup != null)
                {
                    onObjectivePickup(target.transform.position);
                }
            }
            target.transform.SetParent(transform);
			target.transform.localRotation = Quaternion.identity;
			target.transform.localPosition = new Vector3(0f, 1f, 0f);
			target.NotifyPickUp(this);
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

    private bool IsInRange(Vector3 playerPosition, Vector3 interactPosition)
    {
        Vector2 d = interactPosition - playerPosition;
        if (d.sqrMagnitude <= m_interactRadius * m_interactRadius)
        {
            float angle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
            float dAng = Mathf.Abs(Mathf.DeltaAngle(angle - 90f, transform.rotation.eulerAngles.z));

            if (dAng < m_interactArc / 2f)
            {
                return true;
            }
        }
        return false;
    }

    private void TransferPower()
    {
        float beepEnergy = m_beep.GetComponent<PlayerEnergy>().currentEnergy;
        float boopEnergy = m_boop.GetComponent<PlayerEnergy>().currentEnergy;
        float transfer_amount;
        if (m_isBeep)
        {
            if (IsInRange(transform.position, m_boop.transform.position))
            {
                transfer_amount = Mathf.Min(100f - boopEnergy, Mathf.Min(m_powerTransferAmount, Mathf.Max(beepEnergy - 5, 0)));
                m_beep.GetComponent<PlayerEnergy>().currentEnergy -= transfer_amount;
                m_boop.GetComponent<PlayerEnergy>().currentEnergy += transfer_amount;
            }
        }
        else
        {
            if (IsInRange(transform.position, m_beep.transform.position))
            {
                transfer_amount = Mathf.Min(100f - beepEnergy, Mathf.Min(m_powerTransferAmount, Mathf.Max(boopEnergy - 5, 0)));
                m_boop.GetComponent<PlayerEnergy>().currentEnergy -= transfer_amount;
                m_beep.GetComponent<PlayerEnergy>().currentEnergy += transfer_amount;
            }
        }
    }
}
