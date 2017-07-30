using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketUI : MonoBehaviour {

    private GameObject m_boop;
    public Image m_obj1;
    public Image m_obj2;
    public Image m_obj3;
    public Image m_obj4;
    public Image m_obj5;
    private int m_objCount = 0;
    private Image imageComponent;

    void Start()
    {
        bool beepIs1 = false;
        GameObject[] playerList = GameManager.Instance.Players;
        if ((playerList[0]).GetComponent<Player_Navigation>().isBeep)
        {
            beepIs1 = true;
        }
        if (beepIs1)
        {
            m_boop = playerList[1];
        }
        else
        {
            m_boop = playerList[0];
        }
        updateObjectivesUI();
    }


    // Update is called once per frame
    void Update ()
    {
        if(m_objCount != m_boop.GetComponent<PlayerInteraction>().m_objectiveCount)
        {
            m_objCount = m_boop.GetComponent<PlayerInteraction>().m_objectiveCount;
            updateObjectivesUI();
        }
	}
    public void updateObjectivesUI()
    {
        m_obj1.enabled = false;
        m_obj2.enabled = false;
        m_obj3.enabled = false;
        m_obj4.enabled = false;
        m_obj5.enabled = false;
        if (m_objCount > 0)
        {
            m_obj1.enabled = true;
        }
        if (m_objCount > 1)
        {
            m_obj2.enabled = true;
        }
        if (m_objCount > 2)
        {
            m_obj3.enabled = true;
        }
        if (m_objCount > 3)
        {
            m_obj4.enabled = true;
        }
        if (m_objCount > 4)
        {
            m_obj5.enabled = true;
        }
    }
}
