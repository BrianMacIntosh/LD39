using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPortrait : MonoBehaviour
{
	[SerializeField]
	private Image m_portraitImage = null;

	public GameObject SpeechBubbleParent = null;

	public Image SpeechBubbleIcon = null;

	public float HappyPickupShowTime = 2f;

	public float ReminderShowTime = 3f;

	private SceneParent m_sceneParent = null;

	private float m_reminderTimer = 10f;
	private float m_showTime = 0f;

	private SpawnManager m_ghostSpawnManager;
	private ObjectiveManager m_objectiveManager;

	private void Awake()
	{
		if (SceneLoader.Instance)
		{
			SceneLoader.Instance.OnSceneChanged += OnSceneChanged;
		}
		OnSceneChanged(FindObjectOfType<SceneParent>());
	}

	void OnSceneChanged(SceneParent parent)
	{
		m_sceneParent = parent;
		if (m_sceneParent && m_sceneParent.BossNeutralSprite)
		{
			m_portraitImage.sprite = parent.BossNeutralSprite;
			m_portraitImage.enabled = true;
		}
		else
		{
			m_portraitImage.enabled = false;
		}

		GameObject ghostSpawnObject = GameObject.Find("GhostSpawnManager");
		m_ghostSpawnManager = ghostSpawnObject ? ghostSpawnObject.GetComponent<SpawnManager>() : null;

		if (m_objectiveManager)
		{
			m_objectiveManager.OnObjectiveIncremented -= OnObjectiveIncremented;
		}
		m_objectiveManager = FindObjectOfType<ObjectiveManager>();
		if (m_objectiveManager)
		{
			m_objectiveManager.OnObjectiveIncremented += OnObjectiveIncremented;
		}
	}

	void OnObjectiveIncremented()
	{
		m_portraitImage.sprite = m_sceneParent.BossPositiveSprite;
		m_showTime = HappyPickupShowTime;
	}

	private void Update()
	{
		m_reminderTimer -= Time.deltaTime;
		if (m_reminderTimer <= 0f)
		{
			float spawnRateRat = m_ghostSpawnManager ? m_ghostSpawnManager.SpawnRateRatio : 0f;

			// show reminder
			if (m_portraitImage.enabled)
			{
				bool angry = spawnRateRat >= 0.7f;
				SpeechBubbleParent.SetActive(true);
				SpeechBubbleIcon.sprite = angry ? m_sceneParent.ObjectiveSpriteExclame : m_sceneParent.ObjectiveSprite;
				m_portraitImage.sprite = angry ? m_sceneParent.BossNegativeSprite : m_sceneParent.BossNeutralSprite;
				m_showTime = ReminderShowTime;
			}

			// set next reminder timer
			float min = (1f - spawnRateRat) * 10f;
			min += 5f;
			m_reminderTimer = Random.Range(0f, 10f) + min;
		}

		m_showTime -= Time.deltaTime;
		if (m_showTime <= 0f)
		{
			SpeechBubbleParent.SetActive(false);
			if (m_sceneParent.BossNeutralSprite)
			{
				m_portraitImage.sprite = m_sceneParent.BossNeutralSprite;
			}
		}
	}
}
