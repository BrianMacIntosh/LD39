using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveDeposit : MonoBehaviour {

    public Sprite lowGlow;
    public Sprite highGlow;
	// Use this for initialization
	void Start ()
    {
        StartCoroutine(switchSprites());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public IEnumerator switchSprites()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.6f);
            GetComponent<SpriteRenderer>().sprite = lowGlow;
            yield return new WaitForSeconds(0.6f);
            GetComponent<SpriteRenderer>().sprite = highGlow;
        }
    }
}
