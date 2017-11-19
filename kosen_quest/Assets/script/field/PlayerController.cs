using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    public float speed = 3f;
    float moveX = 0f;
    float moveY = 0f;

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
         rb = GetComponent<Rigidbody2D>(); 
		 Debug.Log("Start");
	}

    // Update is called once per frame
    void Update () {
        moveX = Input.GetAxis("Horizontal") * speed;
        moveY = Input.GetAxis("Vertical") * speed;
        Vector3 direction = new Vector3(moveX, moveY, 0);
        rb.velocity = new Vector3(moveX, moveY, 0);


    }
}
