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
    private Image imageComponent;
    // Use this for initialization
    void Start()
    {
        bool beepIs1 = false;
		GameObject[] playerList = GameManager.Instance.Players;
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
        imageComponent = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update ()
    {
        Pickup pickup = player.GetComponent<PlayerInteraction>().m_holdingPickup;

        if (pickup == null)
        {
            imageComponent.enabled = false;
        }
        else if (pickup.CompareTag("Box"))
        {
            imageComponent.enabled = true;
            imageComponent.sprite = boxImage;
        }
        else if (pickup.CompareTag("Water"))
        {
            imageComponent.enabled = true;
            imageComponent.sprite = waterImage;
        }
        else if (pickup.CompareTag("Objective"))
        {
            imageComponent.enabled = true;
            imageComponent.sprite = objectiveImage;
        }
        else
        {
            imageComponent.enabled = false;
        }
    }
}
