using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	[System.Serializable]
	public class SceneInfo
	{
		public string SceneName;
		
		public SceneParent SceneInstance { get; set; }
	}

	[SerializeField]
	private SceneInfo[] m_allScenes = null;

	private void Start()
	{
		// loads in each scene
		foreach (SceneInfo scene in m_allScenes)
		{
			SceneManager.LoadScene(scene.SceneName, LoadSceneMode.Additive);
		}
	}

	public void NotifySceneLoaded(SceneParent parent)
	{
		int index = 0;
		for (int i = 0; i < m_allScenes.Length; i++)
		{
			if (m_allScenes[i].SceneName == parent.SceneName)
			{
				index = i;
				m_allScenes[i].SceneInstance = parent;
				if (index  > 0)
				{
					parent.gameObject.SetActive(false);
				}
				break;
			}
		}

		// hardcoded scene height
		parent.transform.position = new Vector3(0f, 18f * index, 0f);
	}
}
