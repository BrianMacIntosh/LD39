using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Box"))
        {
            Destroy(collision.gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
}
