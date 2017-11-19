using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementKazumasa : MonoBehaviour {

    Animator anim;
    public int Speed;

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();
        Speed = 1;
		
	}
	
	// Update is called once per frame
	void Update () {

        float input_x = Input.GetAxisRaw("Horizontal");
        float input_y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Speed = 2;
        }

        bool isWalking = (Mathf.Abs(input_x) + Mathf.Abs(input_y)) >0;

        anim.SetBool("isWalking", isWalking);
        if (isWalking)
        {
            anim.SetFloat("x", input_x);
            anim.SetFloat("y", input_y);


            if (input_x >= 0.5)
            {
                transform.position += new Vector3(320 * Speed, 0, 0).normalized * Time.deltaTime * 3;
            }

            if (input_y >= 0.5)
            {
                transform.position += new Vector3(0, 320 * Speed, 0).normalized * Time.deltaTime * 3;
            }

            if (input_x <= -0.5)
            {
                transform.position += new Vector3(-320 * Speed, 0, 0).normalized * Time.deltaTime * 3;
            }

            if (input_y <= -0.5)
            {
                transform.position += new Vector3(0, -320 * Speed, 0).normalized * Time.deltaTime * 3;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Speed = 1;
            }

        }

    }
}
