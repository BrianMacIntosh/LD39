using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHealth : MonoBehaviour
{
	void Update()
	{
		if (GhostDamager.AnyContains(transform.position))
		{
			Destroy(gameObject);
		}
	}
}
