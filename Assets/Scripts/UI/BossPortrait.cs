using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPortrait : MonoBehaviour
{
	[SerializeField]
	private Image m_portraitImage = null;

	private SceneParent m_sceneParent = null;

	private void Awake()
	{
		if (SceneLoader.Instance)
		{
			SceneLoader.Instance.OnSceneChanged += OnSceneChanged;
		}
		OnSceneChanged(FindObjectOfType<SceneParent>());
	}

	void OnSceneChanged(SceneParent parent)
	{
		m_sceneParent = parent;
		if (m_sceneParent && m_sceneParent.BossNeutralSprite)
		{
			m_portraitImage.sprite = parent.BossNeutralSprite;
			m_portraitImage.enabled = true;
		}
		else
		{
			m_portraitImage.enabled = false;
		}
	}
}
