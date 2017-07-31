using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePickup : Pickup
{
	protected override void Awake()
	{
		base.Awake();

		SceneParent sceneParent = GetComponentInParent<SceneParent>();
		if (sceneParent)
		{
			GetComponent<SpriteRenderer>().sprite = sceneParent.ObjectiveSprite;
		}
	}
}
