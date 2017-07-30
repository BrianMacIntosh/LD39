using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreatBar : MonoBehaviour {

    GameObject ghostSpawnManager;
    public float threatLevel = 0;
    public float defaultLevel = 0.05f;
    // Use this for initialization
    void Start()
    {
        ghostSpawnManager = GameObject.Find("GhostSpawnManager");

    }

    // Update is called once per frame
    void Update()
    {
        float scaledThreat = (1f - defaultLevel) * (ghostSpawnManager.GetComponent<SpawnManager>().spawnRate - 0.25f);
        GetComponent<Image>().fillAmount = defaultLevel + scaledThreat;
    }
}
