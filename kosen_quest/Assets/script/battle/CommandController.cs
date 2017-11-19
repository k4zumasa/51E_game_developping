using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour {
    

    public void Down() 
    {
        transform.position= new Vector3(transform.position.x,transform.position.y-1.1f,0);
    }
    public void Up()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 1.1f, 0);
    }
    public void First()
    {
        transform.position = new Vector3(transform.position.x, -0.6f, 0);
    }
    public void Last(int i)
    {
        transform.position = new Vector3(transform.position.x, -0.6f-(1.1f*i), 0);
    }
    public void Right()
    {
        transform.position = new Vector3(transform.position.x+0.3f, transform.position.y, 0);
    }
    public void Left()
    {
        transform.position = new Vector3(transform.position.x - 0.3f, transform.position.y, 0);
    }

    public void Down2()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.9f, 0);
    }
    public void Up2()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.9f, 0);
    }
    public void First2()
    {
        transform.position = new Vector3(transform.position.x, -0.4f, 0);
    }
    public void Last2(int i)
    {
        transform.position = new Vector3(transform.position.x, -0.4f - (0.9f * i), 0);
    }


}
