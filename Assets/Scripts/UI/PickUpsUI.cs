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
	
    void Update ()
    {
        Pickup pickup = player.GetComponent<PlayerInteraction>().m_holdingPickup;

        if (pickup == null)
        {
            imageComponent.enabled = false;
        }
        else
        {
            imageComponent.enabled = true;
            imageComponent.sprite = 
				pickup.HudSprite
				? pickup.HudSprite
				: pickup.GetComponentInChildren<SpriteRenderer>().sprite;
        }
    }
}
