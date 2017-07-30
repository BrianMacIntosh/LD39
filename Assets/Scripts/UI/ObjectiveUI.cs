using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveUI : MonoBehaviour
{
	public GameObject BaseObject;

	private List<GameObject> m_children = new List<GameObject>();

	private SceneParent m_sceneParent;
	private ObjectiveManager m_objectiveManager;

	void Start()
	{
		m_children.Add(BaseObject);

		if (SceneLoader.Instance)
		{
			SceneLoader.Instance.OnSceneChanged += OnSceneChanged;
		}
		OnSceneChanged(FindObjectOfType<SceneParent>());
	}

	void OnSceneChanged(SceneParent newScene)
	{
		m_sceneParent = newScene;
		m_objectiveManager = m_sceneParent.GetComponentInChildren<ObjectiveManager>();
	}
	
	void Update()
	{
		int i = 0;
		for (; i < m_objectiveManager.ObjectiveTarget; i++)
		{
			Image child = GetChild(i);
			if (i < m_objectiveManager.ObjectiveProgress)
			{
				child.sprite = m_sceneParent.ObjectiveSprite;
			}
			else
			{
				child.sprite = m_sceneParent.ObjectiveSpriteDim;
			}
		}
		for (; i < m_children.Count; i++)
		{
			m_children[i].SetActive(false);
		}
	}

	private Image GetChild(int i)
	{
		while (i >= m_children.Count)
		{
			GameObject newObj = Instantiate(BaseObject, transform);
			m_children.Add(newObj);
		}
		m_children[i].SetActive(true);
		return m_children[i].GetComponent<Image>();
	}
}
