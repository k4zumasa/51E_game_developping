using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {


	// Use this for initialization
	void Start () {
		 Debug.Log("Start");
	}

    public void OnTriggerStay2D(Collider2D others) {
        Debug.Log("Collid");
        
    }

    // Update is called once per frame
    void Update () {
        bool colid = true;
        if (Input.GetKey(KeyCode.RightArrow) && colid) {
            transform.Translate(0.05f, 0, 0);
        };

        if (Input.GetKey(KeyCode.LeftArrow) && colid) {
            transform.Translate(-0.05f, 0, 0);
        };

		if (Input.GetKey(KeyCode.UpArrow) && colid) {
            transform.Translate(0, 0.05f, 0);
        };

        if (Input.GetKey(KeyCode.DownArrow) && colid) {
            transform.Translate(0, -0.05f, 0);
        };

	}
}
