using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
	public bool isBeep = true;

	void Update()
	{
		GetComponent<Image>().fillAmount = GameManager.Instance.GetPlayer(isBeep).GetComponent<PlayerEnergy>().currentEnergy / 100f;
	}
}
