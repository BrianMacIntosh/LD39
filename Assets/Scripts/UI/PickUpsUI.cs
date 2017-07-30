using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpsUI : MonoBehaviour {

    public bool isBeep = true;
    public GameObject player;
    public Sprite boxImage;
    public Sprite waterImage;
    public Sprite objectiveImage;
    // Use this for initialization
    void Start()
    {
        bool beepIs1 = false;
        GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
        if ((playerList[0]).GetComponent<Player_Navigation>().isBeep)
        {
            beepIs1 = true;
        }
        if ((isBeep && beepIs1) || (!isBeep && !beepIs1))
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
        Pickup pickup = player.GetComponent<PlayerInteraction>().m_holdingPickup;

        if (pickup == null)
        {
            GetComponent<Image>().enabled = false;
        }
        else if (pickup.CompareTag("Box"))
        {
            GetComponent<Image>().enabled = true;
            GetComponent<Image>().sprite = boxImage;
        }
        else if (pickup.CompareTag("Water"))
        {
            GetComponent<Image>().enabled = true;
            GetComponent<Image>().sprite = waterImage;
        }
        else if (pickup.CompareTag("Objective"))
        {
            GetComponent<Image>().enabled = true;
            GetComponent<Image>().sprite = objectiveImage;
        }
        else
        {
            GetComponent<Image>().enabled = false;
        }
    }
}
