using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance
	{
		get
		{
			CheckFindInstance();
			return s_instance;
		}
	}
	private static GameManager s_instance;

	public static void CheckFindInstance()
	{
		if (s_instance == null)
		{
			s_instance = FindObjectOfType<GameManager>();
		}
		if (s_instance == null)
		{
			//HACK: wow
			s_instance = Instantiate(FindObjectOfType<SceneParent>().CameraRigPrefab).GetComponent<GameManager>();
		}
	}

	public GameObject[] Players
	{
		get
		{
			TryFindPlayers();
			return m_players;
		}
	}
	private GameObject[] m_players;

	public GameObject Beep
	{
		get
		{
			TryFindPlayers();
			return m_beep;
		}
	}
	private GameObject m_beep;

	public GameObject Boop
	{
		get
		{
			TryFindPlayers();
			return m_boop;
		}
	}
	private GameObject m_boop;

	public GameObject GetPlayer(bool isBeep)
	{
		if (isBeep)
		{
			return Beep;
		}
		else
		{
			return Boop;
		}
	}

	public void RefindPlayers()
	{
		m_players = null;
		TryFindPlayers();
	}

	private void TryFindPlayers()
	{
		if (m_players == null)
		{
			m_players = GameObject.FindGameObjectsWithTag("Player");

			if (m_players == null || m_players.Length == 0)
			{
				InstantiatePlayers();
				m_players = GameObject.FindGameObjectsWithTag("Player");
			}

			foreach (GameObject obj in m_players)
			{
				if (obj.GetComponent<Player_Navigation>().isBeep)
				{
					m_beep = obj;
				}
				else
				{
					m_boop = obj;
				}
			}
		}
	}

	private void InstantiatePlayers()
	{
		SceneParent sceneParent = FindObjectOfType<SceneParent>();

		PlayerSpawn[] spawnPoints = sceneParent.GetComponentsInChildren<PlayerSpawn>();
		PlayerSpawn beepSpawn = null;
		PlayerSpawn boopSpawn = null;
		foreach (PlayerSpawn spawn in spawnPoints)
		{
			if (spawn.Player == PlayerType.Beep)
			{
				beepSpawn = spawn;
			}
			else if (spawn.Player == PlayerType.Boop)
			{
				boopSpawn = spawn;
			}
		}

		if (!GameManager.Instance.Beep)
		{
			if (!beepSpawn)
			{
				Debug.LogError("No Beep spawn found.");
				Instantiate(sceneParent.BeepPrefab);
			}
			else
			{
				Instantiate(sceneParent.BeepPrefab, beepSpawn.transform.position, beepSpawn.transform.rotation);
			}
		}
		if (!GameManager.Instance.Boop)
		{
			if (!boopSpawn)
			{
				Debug.LogError("No Boop spawn found.");
				Instantiate(sceneParent.BoopPrefab);
			}
			else
			{
				Instantiate(sceneParent.BoopPrefab, boopSpawn.transform.position, boopSpawn.transform.rotation);
			}
		}
	}
}
