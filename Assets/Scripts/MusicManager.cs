using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	public AudioSource SourceLight;

	public AudioSource SourceHeavy;

	public float ThreatThreshold = 0.75f;

	private SpawnManager m_ghostSpawnManager;

	private void Awake()
	{
		if (SceneLoader.Instance)
		{
			SceneLoader.Instance.OnSceneChanged += OnSceneChanged;
		}
	}

	private void OnSceneChanged(SceneParent sceneParent)
	{
		m_ghostSpawnManager = GameObject.Find("GhostSpawnManager").GetComponent<SpawnManager>();
	}

	private void Update()
	{
		//TODO: play heavy music when threat increases
	}
}
