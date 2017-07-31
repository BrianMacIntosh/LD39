using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
	public float Lifetime = 1f;

	private void Awake()
	{
		Destroy(gameObject, Lifetime);
	}
}
