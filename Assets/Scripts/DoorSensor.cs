using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSensor : MonoBehaviour
{
	[Tooltip("The door to control from this sensor.")]
	[SerializeField]
	private Door m_target = null;

	[SerializeField]
	private Sprite m_upSprite = null;

	[SerializeField]
	private Sprite m_downSprite = null;

	[SerializeField]
	private AudioClip m_onDownAudio = null;

	[SerializeField]
	private AudioClip m_onUpAudio = null;

	#region Cached components

	private SpriteRenderer m_spriteRenderer;
	private AudioSource m_audioSource;

	#endregion

	/// <summary>
	/// List of objects that are currently activating this sensor.
	/// </summary>
	private List<GameObject> m_activators = new List<GameObject>();

	private void Awake()
	{
		m_audioSource = OurUtility.GetOrAddComponent<AudioSource>(gameObject);
	}

	private void OnValidate()
	{
		UpdateSprite();
	}

	private void OnDisable()
	{
		m_target.RemoveActiveSensorAll(this);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
		{
			if (m_activators.Count == 0)
			{
				m_target.AddActiveSensor(this);
				m_audioSource.PlayOneShot(m_onDownAudio);
			}
			m_activators.Add(collision.gameObject);
			UpdateSprite();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))
		{
			m_activators.Remove(collision.gameObject);
			if (m_activators.Count == 0)
			{
				m_target.RemoveActiveSensor(this);
				m_audioSource.PlayOneShot(m_onUpAudio);
			}
			UpdateSprite();
		}
	}

	private void UpdateSprite()
	{
		if (m_spriteRenderer == null)
		{
			m_spriteRenderer = GetComponent<SpriteRenderer>();
		}
		m_spriteRenderer.sprite = m_activators.Count > 0 ? m_downSprite : m_upSprite;
	}
}
