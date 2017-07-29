using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Move : MonoBehaviour {

    public float move_speed = 3;
	public float speedMultiplierDuringDamage = 0.2f;
    public float waypointDetectDistance = 15;
    public float waypointMinDistance = 0.2f;
    private Vector3 playerPosition;
    private bool playerSeen = false;
    public GameObject targetPlayer;
    public float timeOut = 1;
    private float timeSinceSeen = 0;
    public GameObject targetWaypoint;
    public bool hasTargetWaypoint = false;
    public Vector3 ghostDirection = new Vector3(0, 1, 0);
    private GameObject[] waypointList;
    private GameObject[] playerList;
    private bool touchingTargetPlayer = false;

    /// <summary>
    /// Returns the ghost's current top movement speed.
    /// </summary>
    private void Start()
    {
        playerList = GameObject.FindGameObjectsWithTag("Player");
        waypointList = GameObject.FindGameObjectsWithTag("GhostPatrolWaypoint");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == targetPlayer)
        {
            touchingTargetPlayer = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == targetPlayer)
        {
            touchingTargetPlayer = false;
        }
    }

    public float CurrentMoveSpeed
	{
		get
		{
			if (m_health.IsBeingDamaged)
			{
				return move_speed * speedMultiplierDuringDamage;
			}
			else
			{
				return move_speed;
			}
		}
	}

	#region Cached Components

	private GhostHealth m_health;

	#endregion

	private void Awake()
	{
		m_health = GetComponent<GhostHealth>();

        GhostSpawnPointManager.Instance.IncrementGhostCount();
	}

    private void OnDestroy()
    {
        if(GhostSpawnPointManager.Instance != null)
        {
            GhostSpawnPointManager.Instance.DecrementGhostCount();
        }
    }

    void Update ()
    {
        if (playerSeen)
        {
            if(!findTargetPlayer())
            {
                timeSinceSeen += Time.deltaTime;
            }
            else
            {
                timeSinceSeen = 0;
            }
            if(timeSinceSeen < timeOut)
            {
                moveTowardTargetPlayerPosition();
            }
            else
            {
                playerSeen = false;
                hasTargetWaypoint = false;
            }
        }
        else
        {
            findClosestPlayer();
            if(!playerSeen)
            {
                if(hasTargetWaypoint)
                {
                    moveTowardTargetWaypoint();
                }
                else
                {
                    getNextTargetWaypoint();
                }
            }
        }
    }

    private bool findTargetPlayer()
    {
        Vector3 dir = targetPlayer.transform.position - transform.position;
        if (!Physics2D.Raycast(transform.position, dir, dir.magnitude, LayerMask.GetMask("Walls")))
        {
            playerPosition = targetPlayer.transform.position;
            return true;
        }
        return false;
    }

    private void moveTowardTargetPlayerPosition()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        Vector3 moveDir = playerPosition - transform.position;
        ghostDirection = moveDir.normalized;
        if(!touchingTargetPlayer)
        {
            rb2d.velocity = CurrentMoveSpeed * moveDir.normalized;
        }
    }
    
    private void findClosestPlayer()
    {
        float distance = 999999999;
        foreach (GameObject player in playerList)
        {
            Vector3 dir = player.transform.position - transform.position;
            if (!Physics2D.Raycast(transform.position, dir, dir.magnitude, LayerMask.GetMask("Walls")))
            {
                if (dir.magnitude < distance)
                {
                    distance = dir.magnitude;
                    playerPosition = player.transform.position;
                    targetPlayer = player;
                    playerSeen = true;
                }
            }
        }
    }

    private void moveTowardTargetWaypoint()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        Vector3 moveDir = targetWaypoint.transform.position - transform.position;
        ghostDirection = moveDir.normalized;
        rb2d.velocity = CurrentMoveSpeed * moveDir.normalized;
        if(moveDir.magnitude <= waypointMinDistance)
        {
            hasTargetWaypoint = false;
        }
    }

    private void getNextTargetWaypoint()
    {
        GameObject[] forwardPoints = new GameObject[waypointList.Length];
        int numForwardPoints = 0;
        GameObject[] backwardsPoints = new GameObject[waypointList.Length];
        int numBackwardsPoints = 0;
        foreach(GameObject waypoint in waypointList)
        {
            Vector3 dir = waypoint.transform.position - transform.position;
            if ((targetWaypoint == null) || (targetWaypoint.transform.position != waypoint.transform.position))
            {
                if (dir.magnitude <= waypointDetectDistance)
                {
                    if (!Physics2D.Raycast(transform.position, dir, dir.magnitude, LayerMask.GetMask("Walls")))
                    {
                        if (Vector2.Dot(((Vector2) dir).normalized, ((Vector2) ghostDirection).normalized) > -Mathf.Cos(20 * Mathf.PI / 180))
                        {
                            forwardPoints[numForwardPoints] = waypoint;
                            numForwardPoints++;
                        }
                        else
                        {
                            backwardsPoints[numBackwardsPoints] = waypoint;
                            numBackwardsPoints++;
                        }
                    }
                }
            }
        }
        if (numForwardPoints > 0)
        {
            int index = ((int)Mathf.Round(Random.value * 100f)) % numForwardPoints;
            targetWaypoint = forwardPoints[index];
            hasTargetWaypoint = true;
        }
        else if (numBackwardsPoints > 0)
        {
            int index = ((int)Mathf.Round(Random.value * 100f)) % numBackwardsPoints;
            targetWaypoint = backwardsPoints[index];
            hasTargetWaypoint = true;
        }
    }
}
