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

	public GameObject Beep { get; private set; }

	public GameObject Boop { get; private set; }

	private void TryFindPlayers()
	{
		if (m_players == null)
		{
			m_players = GameObject.FindGameObjectsWithTag("Player");
			foreach (GameObject obj in m_players)
			{
				if (obj.GetComponent<Player_Navigation>().isBeep)
				{
					Beep = obj;
				}
				else
				{
					Boop = obj;
				}
			}
		}
	}
}
