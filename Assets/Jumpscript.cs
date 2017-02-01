using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpscript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position = new Vector3(0,GetComponent<Animator>().GetFloat("JumpHeight"),0);
        if(transform.position.y > 0.0f)
        {
            transform.position -= new Vector3(0, 0.1f, 0);
        }
	}
}
