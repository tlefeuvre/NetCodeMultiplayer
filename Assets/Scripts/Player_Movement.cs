using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    Vector3 position;
    private float jumpState;
    private bool onground = false;

    // Start is called before the first frame update
    void Start()
    {
        jumpState = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //zqsd
        position = transform.position;

        if (Input.GetKey("z"))
        {
            position.z += 0.08f;
            transform.position = position;
        }
        if (Input.GetKey("q"))
        {
            position.x -= 0.08f;
            transform.position = position;
        }
        if (Input.GetKey("s"))
        {
            position.z -= 0.08f;
            transform.position = position;
        }
        if (Input.GetKey("d"))
        {
            position.x += 0.08f;
            transform.position = position;
        }

        //jump
        if(Input.GetKeyDown("space") && jumpState == 0 && onground == true)
        {
            jumpState = 1;
        }
        
        if(jumpState > 0)
        {
            position.y += 0.11f;
            transform.position = position;

            jumpState -= 0.07f;
            if(jumpState < 0)
            {
                jumpState = 0;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        print("aled");

        if (other.tag == "Ground")
        {
            onground = true;
        }
    }
}
