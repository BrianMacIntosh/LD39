using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
	[SerializeField]
	private PlayerType m_player = PlayerType.Beep;

	public PlayerType Player { get { return m_player; } }

	public static void RespawnAll()
	{
		PlayerSpawn[] spawns = SceneLoader.Instance.CurrentScene.GetComponentsInChildren<PlayerSpawn>();
		foreach (PlayerSpawn spawn in spawns)
		{
			if (spawn.Player == PlayerType.Beep)
			{
				spawn.Respawn(GameManager.Instance.Beep);
			}
			else if (spawn.Player == PlayerType.Boop)
			{
				spawn.Respawn(GameManager.Instance.Boop);
			}
		}
	}

	/// <summary>
	/// Places the specified game object at this spawn point.
	/// </summary>
	public void Respawn(GameObject obj)
	{
		obj.SetActive(true);
		obj.transform.position = transform.position;
		obj.transform.rotation = transform.rotation;
		obj.SendMessage("OnRespawn", SendMessageOptions.DontRequireReceiver);
	}
}
