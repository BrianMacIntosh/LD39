using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour {

    public float currentEnergy = 100;
    public float maxEnergy = 100;
    public float drainRate = 0.25f;
    public int ghostsAttacking = 0;
    public float ghostDrainRate = 5;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentEnergy -= drainRate * Time.deltaTime;
        currentEnergy -= ghostsAttacking * ghostDrainRate * Time.deltaTime;
        if(currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        if(currentEnergy < 0)
        {
            currentEnergy = 0;
        }

	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ghost")
        {
            ghostsAttacking++;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ghost")
        {
            ghostsAttacking--;
        }
    }
}
