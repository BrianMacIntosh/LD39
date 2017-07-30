using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance
	{
		get
		{
			if (s_instance == null)
			{
				s_instance = FindObjectOfType<GameManager>();
			}
			return s_instance;
		}
	}
	private static GameManager s_instance;

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
}
