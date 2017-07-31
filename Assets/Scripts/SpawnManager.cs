using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[System.Serializable]
	public class Spawnable
	{
		[Tooltip("The prefab to spawn.")]
		public GameObject BaseObject;

		[Tooltip("An optional second prefab to also spawn.")]
		public GameObject MirrorObject;

		public float Weight = 1f;
	}

	/// <summary>
	/// List of objects spawned by this manager.
	/// </summary>
	private List<GameObject> m_spawnedObjects = new List<GameObject>();

	[Tooltip("The prefabs to spawn.")]
	public Spawnable[] BaseObjects;

	[Tooltip("In 'objects per second'.")]
	public float baseSpawnRate = 0.25f;

	public float spawnRate = 0.25f;

	public float spawnAcceleration = 0.01f;

	[Tooltip("In 'objects per second'.")]
	public float spawnRateMax = 1;

	public float minSpawnDistance = 2;

	public float maxObjects = 10;

	/// <summary>
	/// Time in seconds this manager has been active for.
	/// </summary>
	private float m_activeTime;

	private Transform[] spawnTransformList;
    private bool[] spawnIsValidList;
    public bool isGhosts = true;
    private bool needsTwoSpawns = false;

	public delegate void ObjectSpawnedDelegate(GameObject obj);

	/// <summary>
	/// Event called when an object is spawned.
	/// </summary>
	public event ObjectSpawnedDelegate OnObjectSpawned;

	/// <summary>
	/// How far the spawn rate has accelerated along its total possible rate (0-1).
	/// </summary>
	public float SpawnRateRatio
	{
		get
		{
			return Mathf.Clamp01((spawnRate - baseSpawnRate) / (spawnRateMax - baseSpawnRate));
		}
	}

	/// <summary>
	/// Returns the number of objects in existence that were spawned by this manager.
	/// </summary>
	private int SpawnedObjectCount
	{
		get
		{
			for (int i = m_spawnedObjects.Count - 1; i >= 0 ; i--)
			{
				if (m_spawnedObjects[i] == null)
				{
					m_spawnedObjects.RemoveAt(i);
				}
			}
			return m_spawnedObjects.Count;
		}
	}

	private float timeSinceLastSpawn = 0;

	void Start()
	{
		spawnTransformList = gameObject.GetComponentsInChildren<Transform>();
        spawnIsValidList = new bool[spawnTransformList.Length];
        for(int i = 0; i < spawnIsValidList.Length; i++)
        {
            spawnIsValidList[i] = true;
        }
        foreach(GameObject player in GameManager.Instance.Players)
        {
            player.GetComponent<PlayerInteraction>().onFireWaterPickup += SetSpawnValid;
            player.GetComponent<PlayerInteraction>().onObjectivePickup += SetSpawnValid;
        }
	}

	void FixedUpdate()
	{
		m_activeTime += Time.deltaTime;
		timeSinceLastSpawn += Time.deltaTime;
		if (spawnRate < spawnRateMax)
		{
			spawnRate = baseSpawnRate + m_activeTime * spawnAcceleration;
		}
		if ((timeSinceLastSpawn > 1.0f / spawnRate) && (SpawnedObjectCount <= maxObjects))
		{
			// add up the total weight available
			float weight = 0f;
			foreach (Spawnable spawnable in BaseObjects)
			{
				weight += spawnable.Weight;
			}

			// select a weight
			float random = Random.Range(0f, weight);

			// spawn the correct entry
			weight = 0f;
			foreach (Spawnable spawnable in BaseObjects)
			{
				weight += spawnable.Weight;
				if (random <= weight)
				{
					if (SpawnObject(spawnable))
					{
						timeSinceLastSpawn = 0;
					}
					break;
				}
			}
		}
	}

	public bool SpawnObject(Spawnable spawnable)
	{
        if (spawnable.MirrorObject != null)
        {
            needsTwoSpawns = true;
        }
		if (SpawnObject(spawnable.BaseObject))
		{
            needsTwoSpawns = false;
            if (spawnable.MirrorObject != null)
			{
				//TODO: it's possible that the second object will fail to spawn. That's bad.
				SpawnObject(spawnable.MirrorObject);
			}
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool SpawnObject(GameObject prefab)
	{
		Transform[] validSpawnTransforms = new Transform[spawnTransformList.Length];
        int[] validSpawnIndexList = new int[spawnTransformList.Length];
		int validSpawnCount = 0;
        int spawnIndex = 0;
		foreach (Transform spawnTransform in spawnTransformList)
		{
            if (spawnIsValidList[spawnIndex])
            {
                if (spawnTransform.transform.position != transform.position)
                {
                    GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
                    Vector3 beepVec = playerList[0].transform.position - spawnTransform.position;
                    Vector3 boopVec = playerList[1].transform.position - spawnTransform.position;
                    if ((beepVec.magnitude > minSpawnDistance) && (boopVec.magnitude > minSpawnDistance))
                    {
                        validSpawnTransforms[validSpawnCount] = spawnTransform;
                        validSpawnIndexList[validSpawnCount] = spawnIndex;
                        validSpawnCount++;
                    }
                }
            }
            spawnIndex++;
		}
		if (((validSpawnCount == 1) && !needsTwoSpawns) || (validSpawnCount > 1))
		{
			int index = ((int)Mathf.Round(Random.value * 100f)) % validSpawnCount;
			GameObject newObject = Instantiate(prefab, validSpawnTransforms[index].position, Quaternion.identity);
			m_spawnedObjects.Add(newObject);
			if (OnObjectSpawned != null) OnObjectSpawned(newObject);
            if(!isGhosts)
            {
                spawnIsValidList[validSpawnIndexList[index]] = false;
            }
			return true;
		}
		return false;
	}

    public void SetSpawnValid(Vector3 position)
    {
        for(int i = 0; i < spawnTransformList.Length; i++)
        {
            if(spawnTransformList[i].position == position)
            {
                spawnIsValidList[i] = true;
                return;
            }
        }
    }
}
