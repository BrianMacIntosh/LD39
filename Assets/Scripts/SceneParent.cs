using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneParent : MonoBehaviour
{
	public string SceneName = "";

	public Sprite BossNeutralSprite;
	public Sprite BossNegativeSprite;
	public Sprite BossPositiveSprite;
	public Sprite ObjectiveSprite;
	public Sprite ObjectiveSpriteDim;

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
		
		GameManager.CheckFindInstance();
	}
}
