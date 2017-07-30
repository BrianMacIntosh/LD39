using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
	[SerializeField]
	private PlayerType m_player = PlayerType.Beep;

	public PlayerType Player { get { return m_player; } }
}
