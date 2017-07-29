using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMove : MonoBehaviour {

    public GameObject beep;
    public float pushSpeed = 6;
    public bool isPushed = false;
	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(isPushed)
        {
            Vector3 direction = transform.position - beep.transform.position;
            GetComponent<Rigidbody2D>().velocity = direction.normalized * pushSpeed;
        }
	}
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == beep)
        {
            isPushed = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        isPushed = false;
    }


}
