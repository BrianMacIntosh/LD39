using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiLoader : MonoBehaviour
{
	private void Start()
	{
		if (!FindObjectOfType<Canvas>())
		{
			SceneManager.LoadScene("Scenes/UI", LoadSceneMode.Additive);
		}
	}
}
