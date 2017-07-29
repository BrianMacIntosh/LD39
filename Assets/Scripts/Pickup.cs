using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Pickup : MonoBehaviour
{
	public static List<Pickup> Pickups = new List<Pickup>();

	[Tooltip("If set, only the specified player can pick this up.")]
	[SerializeField]
	private PlayerType m_onlyPlayer = PlayerType.None;

	public PlayerInteraction HeldBy { get; set; }

#if UNITY_EDITOR
	[UnityEditor.Callbacks.DidReloadScripts]
#endif
	private static void StaticInit()
	{
		if (Pickups == null)
		{
			Pickups = new List<Pickup>();
		}
		else
		{
			Pickups.Clear();
		}
		Pickups.AddRange(FindObjectsOfType<Pickup>());
	}

	private void Awake()
	{
		Pickups.Add(this);
	}

	private void OnDestroy()
	{
		Pickups.Remove(this);
	}

	/// <summary>
	/// Returns true if the specified player can grab this.
	/// </summary>
	public bool CanBeGrabbedBy(PlayerType playerType)
	{
		if (m_onlyPlayer == PlayerType.None)
		{
			return true;
		}
		else
		{
			return m_onlyPlayer == playerType;
		}
	}
}
