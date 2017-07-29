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

	private Transform[] spawnTransformList;

	public delegate void ObjectSpawnedDelegate(GameObject obj);

	/// <summary>
	/// Event called when an object is spawned.
	/// </summary>
	public event ObjectSpawnedDelegate OnObjectSpawned;

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
	}

	void FixedUpdate()
	{
		timeSinceLastSpawn += Time.deltaTime;
		if (spawnRate < spawnRateMax)
		{
			spawnRate = baseSpawnRate + Time.time * spawnAcceleration;
		}
		if ((timeSinceLastSpawn > 1.0f / spawnRate) && (SpawnedObjectCount <= maxObjects))
		{
			Spawnable spawnPrefab = BaseObjects[Random.Range(0, BaseObjects.Length)];
			if (SpawnObject(spawnPrefab))
			{
				timeSinceLastSpawn = 0;
			}
		}
	}

	public bool SpawnObject(Spawnable spawnable)
	{
		if (SpawnObject(spawnable.BaseObject))
		{
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
		int validSpawnCount = 0;
		foreach (Transform spawnTransform in spawnTransformList)
		{
			if (spawnTransform.transform.position != transform.position)
			{
				GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
				Vector3 beepVec = playerList[0].transform.position - spawnTransform.position;
				Vector3 boopVec = playerList[1].transform.position - spawnTransform.position;
				if ((beepVec.magnitude > minSpawnDistance) && (boopVec.magnitude > minSpawnDistance))
				{
					validSpawnTransforms[validSpawnCount] = spawnTransform;
					validSpawnCount++;
				}
			}
		}
		if (validSpawnCount > 0)
		{
			int index = ((int)Mathf.Round(Random.value * 100f)) % validSpawnCount;
			GameObject newObject = Instantiate(prefab, validSpawnTransforms[index].position, Quaternion.identity);
			m_spawnedObjects.Add(newObject);
			if (OnObjectSpawned != null) OnObjectSpawned(newObject);
			return true;
		}
		return false;
	}
}
