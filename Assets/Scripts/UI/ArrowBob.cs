using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBob : MonoBehaviour {

    public float bobAmount = 0.07f;
    public float bobRate = 1;
    public float baseYPosition = 0.9f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y + baseYPosition + bobAmount * Mathf.Sin(2 * Mathf.PI * Time.time * bobRate), transform.parent.position.z);
        transform.rotation = Quaternion.identity;
    }

}
