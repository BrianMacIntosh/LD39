using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Move : MonoBehaviour {

    public float move_speed = 3;
    public float detectDistance= 10;
    private Vector3 playerPosition;
    private bool playerSeen = false;
    private GameObject targetPlayer;
    public float timeOut = 1;
    private float timeSinceSeen = 0;
	
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
            }
        }
        else
        {
            findClosestPlayer();
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
    
}
