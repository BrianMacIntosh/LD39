using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
	[SerializeField]
    private int m_objectiveCount = 0;
	[SerializeField]
	private int m_objectiveMax = 20;

	public int ObjectiveProgress { get { return m_objectiveCount; } }
	public int ObjectiveTarget { get { return m_objectiveMax; } }

    void Start ()
    {
		foreach (GameObject player in GameManager.Instance.Players)
        {
            player.GetComponent<PlayerInteraction>().depositedObjects += addObjectives;
        }
	}
	
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
        m_objectiveCount = Mathf.Min(m_objectiveCount, m_objectiveMax);
    }
}

