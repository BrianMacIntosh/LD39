using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawnPointManager : MonoBehaviour {
    public static GhostSpawnPointManager Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = FindObjectOfType<GhostSpawnPointManager>();
            }
            return s_instance;
        }
    }
    private static GhostSpawnPointManager s_instance;

    public GameObject Ghost;
    public float baseSpawnRate = 0.25f;
    public float spawnRate = 0.25f;
    public float spawnAcceleration = 0.01f;
    public float spawnRateMax = 1;
    public float minSpawnDistance = 2;
    public float maxGhosts = 10;
    private int ghostCount;
    private Transform[] spawnTransformList;

    private float timeSinceLastSpawn = 0;
	// Use this for initialization
	void Start () {
        spawnTransformList = gameObject.GetComponentsInChildren<Transform>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        timeSinceLastSpawn += Time.deltaTime;
        if(spawnRate < spawnRateMax)
        {
            spawnRate = baseSpawnRate + Time.time * spawnAcceleration;
        }
        if((timeSinceLastSpawn > 1.0f/spawnRate) && (ghostCount <= maxGhosts))
        {
            if (spawnGhost())
            {
                timeSinceLastSpawn = 0;
            }
        }
	}
    public bool spawnGhost()
    {
        Transform[] validSpawnTransforms = new Transform[spawnTransformList.Length];
        int validSpawnCount = 0;
        foreach (Transform spawnTransform in spawnTransformList)
        {
            if (spawnTransform.transform.position != transform.position)
            {
                GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
                Vector3 beepVec = playerList[0].transform.position - spawnTransform.position;
                Vector3 boopVec = playerList[1].transform.position - spawnTransform.position;
                if((beepVec.magnitude > minSpawnDistance) && (boopVec.magnitude > minSpawnDistance))
                {
                    validSpawnTransforms[validSpawnCount] = spawnTransform;
                    validSpawnCount++;
                }
            }
        }
        if (validSpawnCount > 0)
        {
            int index = ((int)Mathf.Round(Random.value * 100f)) % validSpawnCount;
            Instantiate(Ghost, validSpawnTransforms[index].position, Quaternion.identity);
            return true;
        }
        return false;
    }
    public void IncrementGhostCount()
    {
        ghostCount++;
    }
    public void DecrementGhostCount()
    {
        ghostCount--;
    }
}
