using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Navigation : MonoBehaviour {

    public float move_speed = 5;
    public float turn_speed = 100;
    public float dirDegAngle = 90;
    public bool isBeep = true;

    // Update is called once per frame
    void Update ()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        float turn;
        float moveForward;
        if (isBeep)
        {
            turn = Input.GetAxis("BeepHorizontal");
            moveForward = Input.GetAxis("BeepVertical");
        }
        else
        {
            turn = Input.GetAxis("BoopHorizontal");
            moveForward = Input.GetAxis("BoopVertical");
        }
   
        Vector2 direction = GetVector2FromAngle(dirDegAngle);
        Vector2 moveDirection = new Vector2(direction[0]*moveForward, direction[1]*moveForward);

        rb2d.velocity = moveDirection * move_speed;
        rb2d.angularVelocity = -turn * turn_speed;
        dirDegAngle = transform.rotation.eulerAngles.z + 90;

	}

    private Vector2 GetVector2FromAngle(float degAngle)
    {
        float radAngle = degAngle * Mathf.PI / 180;
        return new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle));
    }
}
