using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	public static SceneLoader Instance
	{
		get
		{
			if (s_instance == null)
			{
				s_instance = FindObjectOfType<SceneLoader>();
			}
			return s_instance;
		}
	}
	private static SceneLoader s_instance = null;

	[System.Serializable]
	public class SceneInfo
	{
		public string SceneName;
		
		public SceneParent SceneInstance { get; set; }
	}

	private int m_currentScene = -1;

	[SerializeField]
	private SceneInfo[] m_allScenes = null;

	public SceneParent CurrentScene
	{
		get { return m_allScenes[m_currentScene].SceneInstance; }
	}

	public delegate void SceneChanged(SceneParent newScene);
	public event SceneChanged OnSceneChanged;

	private void Start()
	{
		// loads in each scene
		foreach (SceneInfo scene in m_allScenes)
		{
			SceneManager.LoadScene(scene.SceneName, LoadSceneMode.Additive);
		}

		StartUpUi();
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
				parent.gameObject.SetActive(false);
				break;
			}
		}

		// hardcoded scene height
		parent.transform.position = new Vector3(0f, 18f * (index + 1f), 0f);
	}

	public void NextScene()
	{
		if (m_currentScene >= 0)
		{
			m_allScenes[m_currentScene].SceneInstance.gameObject.SetActive(false);
		}
		m_currentScene++;
		if (m_currentScene < m_allScenes.Length)
		{
			SceneParent sceneParent = m_allScenes[m_currentScene].SceneInstance;
			sceneParent.gameObject.SetActive(true);
			PlayerSpawn.RespawnAll();
			CameraControl.Instance.GoTo(sceneParent.transform.position);

			if (OnSceneChanged != null)
			{
				OnSceneChanged(sceneParent);
			}
		}
	}

	private void StartUpUi()
	{
		UiMaster ui = FindObjectOfType<UiMaster>();
		if (ui)
		{
			ui.InGameUI.SetActive(false);
			ui.FrontEndUI.SetActive(true);
		}
		else
		{
			UiMaster.OnUiLoaded -= OnUiLoaded;
			UiMaster.OnUiLoaded += OnUiLoaded;
		}
	}

	void OnUiLoaded()
	{
		StartUpUi();
	}
}
