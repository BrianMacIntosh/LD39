using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Move : MonoBehaviour {

    public float move_speed = 3;
    public float detectDistance= 10;
    public float waypointDetectDistance = 15;
    public float waypointMinDistance = 0.2f;
    private Vector3 playerPosition;
    private bool playerSeen = false;
    private GameObject targetPlayer;
    public float timeOut = 1;
    private float timeSinceSeen = 0;
    public GameObject targetWaypoint;
    public bool hasTargetWaypoint = false;
    public Vector3 ghostDirection = new Vector3(0, 1, 0);
	
	// Update is called once per frame
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
        if (!Physics2D.Raycast(transform.position, dir, detectDistance, LayerMask.GetMask("Walls")))
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
        rb2d.velocity = move_speed * moveDir.normalized;
    }
    
    private void findClosestPlayer()
    {
        float distance = 999999999;
        GameObject[] playerList = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in playerList)
        {
            Vector3 dir = player.transform.position - transform.position;
            if (!Physics2D.Raycast(transform.position, dir, detectDistance, LayerMask.GetMask("Walls")))
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
        rb2d.velocity = move_speed * moveDir.normalized;
        if(moveDir.magnitude < waypointMinDistance)
        {
            hasTargetWaypoint = false;
        }
    }

    private void getNextTargetWaypoint()
    {

        GameObject[] waypointList = GameObject.FindGameObjectsWithTag("GhostPatrolWaypoint");
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
                    if (!Physics2D.Raycast(transform.position, dir, waypointDetectDistance, LayerMask.GetMask("Walls")))
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
