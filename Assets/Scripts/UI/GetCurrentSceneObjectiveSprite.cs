using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCurrentSceneObjectiveSprite : MonoBehaviour
{
	private Image m_image = null;

	void Start()
	{
		m_image = GetComponent<Image>();
		
		if (SceneLoader.Instance)
		{
			SceneLoader.Instance.OnSceneChanged += OnSceneChanged;
		}
		OnSceneChanged(FindObjectOfType<SceneParent>());
	}

	void OnSceneChanged(SceneParent newScene)
	{
		m_image.sprite = newScene.ObjectiveSprite;
	}
}
