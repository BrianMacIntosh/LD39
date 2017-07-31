using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
	public GameObject Content;

	private void Start()
	{
		Content.SetActive(false);
	}

	private void Update()
	{
		if (Content.activeSelf)
		{
			if (Input.GetButton("Start"))
			{
				SceneLoader.Instance.ReloadCurrentScene();
			}
		}
		else
		{
			bool anyHasEnergy = false;
			foreach (GameObject player in GameManager.Instance.Players)
			{
				if (player.GetComponent<PlayerEnergy>().HasEnergy)
				{
					anyHasEnergy = true;
					break;
				}
			}
			if (!anyHasEnergy)
			{
				Content.SetActive(true);
			}
		}
	}
}
