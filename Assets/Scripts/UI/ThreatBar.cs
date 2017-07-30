using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreatBar : MonoBehaviour
{
	GameObject ghostSpawnManager;
	public float threatLevel = 0;
	public float defaultLevel = 0.05f;

	void Start()
	{
		ghostSpawnManager = GameObject.Find("GhostSpawnManager");
	}

	void Update()
	{
		float scaledThreat = (1f - defaultLevel) * (ghostSpawnManager.GetComponent<SpawnManager>().SpawnRateRatio);
		GetComponent<Image>().fillAmount = defaultLevel + scaledThreat;
	}
}
