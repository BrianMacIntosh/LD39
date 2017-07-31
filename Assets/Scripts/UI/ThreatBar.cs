using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreatBar : MonoBehaviour
{
	SpawnManager ghostSpawnManager;
	public float threatLevel = 0;
	public float defaultLevel = 0.05f;
	
	[SerializeField]
	private Image m_fillImage = null;

	[SerializeField]
	private GameObject m_elementParent = null;

	void Start()
	{
		if (SceneLoader.Instance)
		{
			SceneLoader.Instance.OnSceneChanged += OnSceneChanged;
		}
		OnSceneChanged(FindObjectOfType<SceneParent>());
	}

	void OnSceneChanged(SceneParent parent)
	{
		GameObject ghostSpawner = GameObject.Find("GhostSpawnManager");
		ghostSpawnManager = ghostSpawner ? ghostSpawner.GetComponent<SpawnManager>() : null;
		m_elementParent.gameObject.SetActive(ghostSpawnManager != null);
	}

	void Update()
	{
		if (ghostSpawnManager)
		{
			float scaledThreat = (1f - defaultLevel) * (ghostSpawnManager.SpawnRateRatio);
			m_fillImage.fillAmount = defaultLevel + scaledThreat;
		}
		else
		{
			m_fillImage.fillAmount = 0f;
		}
	}
}
