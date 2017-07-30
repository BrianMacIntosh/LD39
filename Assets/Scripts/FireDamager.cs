using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamager : MonoBehaviour {

    public float damageRate = 5; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerEnergy>().AddEnergy(-damageRate * Time.deltaTime);
        }
    }
}
