using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
	public bool isBeep = true;
	public GameObject player;

	void Start()
	{
		if (isBeep)
		{
			player = GameManager.Instance.Beep;
		}
		else
		{
			player = GameManager.Instance.Boop;
		}
	}

	void Update()
	{
		GetComponent<Image>().fillAmount = player.GetComponent<PlayerEnergy>().currentEnergy / 100f;
	}
}
