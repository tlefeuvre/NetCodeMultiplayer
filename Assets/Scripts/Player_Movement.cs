using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Player_Movement : NetworkBehaviour
{

    Vector3 position;

    private float jumpState;
    private bool onground = false;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        jumpState = 0;
        animator = GetComponentInChildren<Animator>();
    }
    /*public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            Destroy(transform.GetComponent<Player_Movement>());
        }
    }*/
    /*public override void OnNetworkSpawn()
    {
        if (!IsOwner)
            Destroy(this);
    }*/
    // Update is called once per frame
    void FixedUpdate()
    {
        //zqsd
        position = transform.position;

        if (Input.GetKey("z"))
        {
            Debug.Log("zzzzz");
            animator.SetBool("IsWalking", true);

            position.z += 0.08f;
            transform.position = position;
        }
        if (Input.GetKey("q"))
        {
            animator.SetBool("IsWalking", true);

            position.x -= 0.08f;
            transform.position = position;
        }
        if (Input.GetKey("s"))
        {
            animator.SetBool("IsWalking", true);

            position.z -= 0.08f;
            transform.position = position;
        }
        if (Input.GetKey("d"))
        {
            animator.SetBool("IsWalking", true);

            position.x += 0.08f;
            transform.position = position;
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        //jump
        if (Input.GetKeyDown("space") && jumpState == 0 && onground == true)
        {
            jumpState = 1;
        }

        if (jumpState > 0)
        {
            position.y += 0.11f;
            transform.position = position;

            jumpState -= 0.07f;
            if (jumpState < 0)
            {
                jumpState = 0;
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            onground = true;
        }
    }
}
