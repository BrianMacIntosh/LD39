using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
	[Tooltip("The amount of energy the pickup restores.")]
	[SerializeField]
	private float m_energy = 1f;
}
