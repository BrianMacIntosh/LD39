using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
	public GameObject Contents;

	private void Start()
	{
		Contents.SetActive(false);
	}

	void Update()
	{
		bool pauseControl = Input.GetButtonDown("Pause");
		bool startControl = Input.GetButtonDown("Start");
		if (Time.timeScale > 0f && pauseControl)
		{
			Contents.SetActive(true);
			Time.timeScale = 0f;
		}
		else if (Time.timeScale < 1f && (pauseControl || startControl))
		{
			Contents.SetActive(false);
			Time.timeScale = 1f;
		}
	}
}
