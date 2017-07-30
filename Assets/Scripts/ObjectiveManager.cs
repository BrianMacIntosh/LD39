using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public int m_objectiveCount = 0;
    public int m_objectiveMax = 20;


// Use this for initialization
    void Start ()
    {
		foreach (GameObject player in GameManager.Instance.Players)
        {
            player.GetComponent<PlayerInteraction>().depositedObjects += addObjectives;
            player.GetComponent<PlayerInteraction>().depositedObjects += addObjectives;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(m_objectiveCount >= m_objectiveMax)
        {
            SceneLoader.Instance.NextScene();
        }
    }

    public void addObjectives(int num)
    {
        m_objectiveCount += num;
        m_objectiveCount = Mathf.Min(num, m_objectiveMax);
    }
}

