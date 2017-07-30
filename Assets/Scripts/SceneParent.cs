using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneParent : MonoBehaviour
{
	public string SceneName = "";

	public GameObject CameraRigPrefab;
	public GameObject BoopPrefab;
	public GameObject BeepPrefab;
	
	private void Start()
	{
		SceneLoader loader = FindObjectOfType<SceneLoader>();
		if (loader != null)
		{
			loader.NotifySceneLoaded(this);
		}

		PlayerSpawn[] spawnPoints = FindObjectsOfType<PlayerSpawn>();
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

		if (!FindObjectOfType<GameManager>())
		{
			Instantiate(CameraRigPrefab);
		}
		if (!GameManager.Instance.Beep)
		{
			if (!beepSpawn)
			{
				Debug.LogError("No Beep spawn in scene '" + SceneName + "'.");
			}
			else
			{
				Instantiate(BeepPrefab, beepSpawn.transform.position, beepSpawn.transform.rotation);
			}
		}
		if (!GameManager.Instance.Boop)
		{
			if (!boopSpawn)
			{
				Debug.LogError("No Boop spawn in scene '" + SceneName + "'.");
			}
			else
			{
				Instantiate(BoopPrefab, boopSpawn.transform.position, boopSpawn.transform.rotation);
			}
		}
	}
}
