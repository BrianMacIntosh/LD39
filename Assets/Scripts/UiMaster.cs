using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMaster : MonoBehaviour
{
	public static UiMaster Instance
	{
		get
		{
			if (s_instance == null)
			{
				s_instance = FindObjectOfType<UiMaster>();
			}
			return s_instance;
		}
	}
	private static UiMaster s_instance = null;

	public GameObject InGameUI;

	public GameObject FrontEndUI;

	public delegate void UiLoadedDelegate();
	public static event UiLoadedDelegate OnUiLoaded;

	private void Start()
	{
		if (OnUiLoaded != null)
		{
			OnUiLoaded();
		}
	}

	private void Update()
	{
		// anchor to the camera
		Vector3 position = CameraControl.Instance.transform.position;
		position.z = transform.position.z;
		transform.position = position;
	}
}
