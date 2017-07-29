using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour {
    public bool isBeep = true;
    public GameObject player;
	// Use this for initialization
	void Start ()
    {
        bool beepIs1 = false;
        GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
        if((playerList[0]).GetComponent<Player_Navigation>().isBeep)
        {
            beepIs1 = true;
        }
        if((isBeep && beepIs1) || (!isBeep && !beepIs1))
        {
            player = playerList[0];
        }
        else
        {
            player = playerList[1];
        }

	}
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<Image>().fillAmount = player.GetComponent<PlayerEnergy>().currentEnergy / 100f;
	}
}
