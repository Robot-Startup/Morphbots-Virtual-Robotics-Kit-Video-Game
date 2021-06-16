using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class PongControls : MonoBehaviour
{
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public float speed = 10.0f;
    public float boundZ = 2.25f;
    public Rigidbody rb;
    
    // Start is called before the first frame update
   void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PaddleMove();
    }

    //player 1 controls
    public void PaddleMove()
    {
        var vel = rb.velocity;

        if (Input.GetKey(moveUp))
        {  
            vel.z = speed;
        }
        else if (Input.GetKey(moveDown))
        {
            vel.z = -speed;
        }
        else
        {
            vel.z = 0;
        }
        rb.velocity = vel;

        var pos = transform.position;
        if (pos.z > boundZ)
        {
            pos.z = boundZ;
        }
        else if (pos.z < -boundZ)
        {
            pos.z = -boundZ;
        }
        transform.position = pos;
    }
}*/

public class PongControls : MonoBehaviour
{
    public bool isPlayer1;
    public float speed = 5f;

    void Start(){    
    }

    void Update(){
        if (isPlayer1){
            transform.Translate (0f, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0f);
        }
        else {
            transform.Translate (0f, Input.GetAxis("Vertical2") * speed * Time.deltaTime, 0f);
        }
    }
}