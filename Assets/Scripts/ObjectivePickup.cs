using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePickup : Pickup
{
	private void Awake()
	{
		SceneParent sceneParent = GetComponentInParent<SceneParent>();
		if (sceneParent)
		{
			GetComponent<SpriteRenderer>().sprite = sceneParent.ObjectiveSprite;
		}
	}
}
