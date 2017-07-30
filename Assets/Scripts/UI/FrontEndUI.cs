using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontEndUI : MonoBehaviour
{
	void Update()
	{
		if (Input.GetButtonDown("Start"))
		{
			// start the game
			UiMaster.Instance.FrontEndUI.SetActive(false);
			UiMaster.Instance.InGameUI.SetActive(true);
			SceneLoader.Instance.NextScene();
		}
	}
}
